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
using Friends.Entities;

namespace Friends.Repository
{
    public class UsuariosRepository : SQLServerContext, IUsuariosRepository
    {
        public UsuariosRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<Usuarios> Authenticate(string Username, string PassWord)
        {
            string sQuery = "select * from usuarios where username=@username and password=@password";

            var result = await Connection.QueryAsync<Usuarios>(sQuery, 
                param: new { username = Username, password = PassWord }, 
                transaction: Transaction);

            return result.FirstOrDefault();
        }

    }
}
