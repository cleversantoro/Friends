using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Device.Location;
using Google.Maps;
using Google.Maps.Geocoding;

namespace viavarejofriends
{
    public class Repository
    {
        public static List<Person> GetPersons()
        {
            JsonSerializer serializer = new JsonSerializer();
            List<Person> listPerson = new List<Person>();

            string path = System.IO.Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(string.Format("{0}{1}", path, "\\data\\person.txt")))
            {
                string json = sr.ReadToEnd();
                listPerson = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            return listPerson;
        }

        public static Person GetLatLongtoAddress(Person person)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAeuMG_ryGN1_JVLUVnVbNedMrOswNAO8g")); //KEY(chave) do google maps

            var request = new GeocodingRequest();
            request.Address = String.Format("{0},{1},{2},{3},{4}",
                person.endereco.logradouro,
                person.endereco.numero,
                person.endereco.cidade,
                person.endereco.estado,
                person.endereco.pais
                );

            var response = new GeocodingService().GetResponse(request);
            if (response.Status == ServiceResponseStatus.Ok && response.Results.Count() > 0)
            {
                var result = response.Results.First();

                person.endereco.latitude = result.Geometry.Location.Latitude;
                person.endereco.longitude = result.Geometry.Location.Longitude;
            }
            else
            {
                Console.WriteLine("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
            }

            return person;
        }

        public static Person GetPersonByName(string nome, string sobrenome)
        {
            JsonSerializer serializer = new JsonSerializer();
            List<Person> listPerson = new List<Person>();

            string path = System.IO.Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(string.Format("{0}{1}", path, "\\data\\person.txt")))
            {
                string json = sr.ReadToEnd();
                listPerson = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            Person pers = listPerson.Where(p => p.nome.Equals(nome) && p.sobrenome.Equals(sobrenome)).First();
            return pers;
        }

        public static List<Person> GetCloseFriends(string nome, string sobrenome)
        {
            JsonSerializer serializer = new JsonSerializer();
            List<Person> listPerson = new List<Person>();
            List<Person> retorno = new List<Person>();

            string path = System.IO.Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(string.Format("{0}{1}", path, "\\data\\person.txt")))
            {
                string json = sr.ReadToEnd();
                List<Person> list = JsonConvert.DeserializeObject<List<Person>>(json);

                foreach (Person item in list)
                {
                    listPerson.Add(GetLatLongtoAddress(item));
                }
            }
            
            Person porigem = listPerson.Where(p => p.nome.Equals(nome) && p.sobrenome.Equals(sobrenome)).FirstOrDefault();

            var res = from tc in listPerson 
                      select new 
                      {
                          person = tc,       
                          distance = GetDistanceLatLong(porigem,tc),
                      };

            var t = res.OrderBy(p=> p.distance).Where(p=> p.distance > 0 ).Take(3).ToList();

            foreach (var item in t)
            {
                retorno.Add(item.person);
            }

            return retorno;
        }

        public static double GetDistanceLatLong(Person porigem, Person pdestino)
        {
            var sCoord = new GeoCoordinate(porigem.endereco.latitude, porigem.endereco.longitude);
            var eCoord = new GeoCoordinate(pdestino.endereco.latitude, pdestino.endereco.longitude);

            return sCoord.GetDistanceTo(eCoord);
        }
    }
}
