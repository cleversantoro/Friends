using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Friends.Repository.Context;
using Friends.Repository.Interface;
using Friends.Domain.Entities;
using Google.Maps;
using Google.Maps.Geocoding;
using Newtonsoft.Json;
using System.IO;
using GeoCoordinatePortable;
using Dapper;
using System.Globalization;

namespace Friends.Repository
{
    public class ContatosRepository : SQLServerContext, IContatosRepository
    {
        public ContatosRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public Contatos GetLatLongtoAddress(Contatos Contatos)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAeuMG_ryGN1_JVLUVnVbNedMrOswNAO8g")); //KEY(chave) do google maps

            var request = new GeocodingRequest
            {
                Address = string.Format("{0},{1},{2},{3},{4}",
                Contatos.endereco.Logradouro,
                Contatos.endereco.Numero,
                Contatos.endereco.Cidade,
                Contatos.endereco.Estado,
                Contatos.endereco.Pais
                )
            };

            var response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();

                Contatos.endereco.Latitude = result.Geometry.Location.Latitude.ToString();
                Contatos.endereco.Longitude = result.Geometry.Location.Longitude.ToString();
                UpdateLatLong(Contatos.endereco);
            }
            else
            {
                Console.WriteLine("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
            }

            return Contatos;
        }

        public void UpdateLatLong(Enderecos entity)
        {
            Connection.Execute(
                "UPDATE Enderecos SET Latitude = @latitude, Longitude = @longitude WHERE id = @id",
                param: new { Latitude = entity.Latitude, Longitude = entity.Longitude, id = entity.Id },
                transaction: Transaction
            );
        }

        public void InsertHistoricoLog(int idcontato, int idamigo, string distancia)
        {
            string sQuery = "INSERT INTO " +
                "CalculoHistoricoLog (id_Contato,id_Amigo,Distancia) " +
                "VALUES (@id_Contato, @id_Amigo, @Distancia);";

            var parametros = new DynamicParameters();
            parametros.Add("id_Contato", idcontato, DbType.Int32);
            parametros.Add("id_Amigo", idamigo, DbType.Int32);
            parametros.Add("Distancia", distancia, DbType.String);

            Connection.Execute(
                sQuery,
                param: parametros,
                transaction: Transaction
            );
        }

        private IEnumerable<ContatosViewModel> GetCloseFriends(int id)
        {
            JsonSerializer serializer = new JsonSerializer();
            List<Contatos> listContatos = new List<Contatos>();
            List<ContatosViewModel> retorno = new List<ContatosViewModel>();

            string sQuery = "select c.*,e.* " +
                            "from contatos c " +
                            "inner join enderecos e on c.id_endereco = e.id";

            var result = Connection.Query<Contatos, Enderecos, Contatos>(sQuery,
                map: (contatos, enderecos) =>
                {
                    contatos.endereco = enderecos;
                    return contatos;
                },
                splitOn: "id_endereco,id",
                transaction: Transaction);

            foreach (var item in result)
            {
                if ((string.IsNullOrEmpty(item.endereco.Longitude)) || (string.IsNullOrEmpty(item.endereco.Latitude)))
                    listContatos.Add(GetLatLongtoAddress(item));
                else
                    listContatos.Add(item);
            }

            Contatos porigem = listContatos.Where(p => p.Id == id).FirstOrDefault();

            var res = from tc in listContatos
                      select new
                      {
                          Contatos = tc,
                          distance = GetDistanceLatLong(porigem, tc),
                      };

            var t = res.OrderBy(p => p.distance).Where(p => p.distance > 0).Take(3).ToList();

            foreach (var item in t)
            {
                retorno.Add(new ContatosViewModel()
                {
                    Cidade = item.Contatos.endereco.Cidade,
                    Distancia = Math.Round(double.Parse(item.distance.ToString())).ToString(),
                    Estado = item.Contatos.endereco.Estado,
                    Logradouro = item.Contatos.endereco.Logradouro,
                    Nome = item.Contatos.Nome,
                    Numero = item.Contatos.endereco.Numero,
                    Pais = item.Contatos.endereco.Pais,
                    Sobrenome = item.Contatos.Sobrenome
                });

                InsertHistoricoLog(porigem.Id, item.Contatos.Id, item.distance.ToString());
            }
            Transaction.Commit();

            return retorno;
        }

        public double GetDistanceLatLong(Contatos porigem, Contatos pdestino)
        {
            var sCoord = new GeoCoordinate(double.Parse(porigem.endereco.Latitude, CultureInfo.InvariantCulture), double.Parse(porigem.endereco.Longitude, CultureInfo.InvariantCulture));
            var eCoord = new GeoCoordinate(double.Parse(pdestino.endereco.Latitude, CultureInfo.InvariantCulture), double.Parse(pdestino.endereco.Longitude, CultureInfo.InvariantCulture));

            return sCoord.GetDistanceTo(eCoord);
        }

        public async Task<IEnumerable<ContatosListViewModel>> SelectList()
        {
            string sQuery = "SELECT c.id, c.nome+ ' ' +c.Sobrenome AS nome " +
                            "FROM Contatos c ";

            var result = await Connection.QueryAsync<ContatosListViewModel>(
                sQuery, transaction: Transaction);

            return result.ToList();
        }

        public async Task<Contatos> GetByID(int id)
        {
            string sQuery = "select c.id, c.nome, c.sobrenome, e.* " +
                            "from contatos c " +
                            "inner join enderecos e on c.id_endereco = e.id" +
                            "where c.id = @id";

            var result = await Connection.QueryAsync<Contatos, Enderecos, Contatos>(
                sQuery,
                param: new { id = id },
                map: (contatos, enderecos) =>
                {
                    contatos.endereco = enderecos;
                    return contatos;
                },
                splitOn: "id_endereco,id",
                transaction: Transaction);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Contatos>> SelectAll()
        {
            string sQuery = "SELECT c.id, id_endereco, c.nome, c.sobrenome, e.* " +
                            "FROM Contatos c inner join Enderecos e on c.id_Endereco = e.id";

            var result = await Connection.QueryAsync<Contatos, Enderecos, Contatos>(
                sQuery,
                map: (contatos, enderecos) =>
                {
                    contatos.endereco = enderecos;
                    return contatos;
                },
                splitOn: "id_endereco,id",
                transaction: Transaction);

            return result.ToList();
        }

        public List<ContatosViewModel> GetCloseContacts(int id)
        {
            var result = GetCloseFriends(id);
            return result.ToList();
        }
    }
}
