using System;
using System.Linq;
using Google.Maps;
using Google.Maps.Geocoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Device.Location;
using viavarejofriends;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace viavarejofriendsTest
{
    [TestClass]
    public class FriendsTest
    {
        [TestMethod]
        public void GetLatLongtoAddressTest()
        {
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAeuMG_ryGN1_JVLUVnVbNedMrOswNAO8g")); //KEY(chave) do google maps

            Person person = new Person();
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
                
                person.endereco.latitude= result.Geometry.Location.Latitude;
                person.endereco.longitude = result.Geometry.Location.Longitude;
            }
            else
            {
                Console.WriteLine("Unable to geocode.  Status={0} and ErrorMessage={1}", response.Status, response.ErrorMessage);
            }

            //return person;

            Assert.Equals(person.endereco.latitude, -46.20);
            Assert.Equals(person.endereco.latitude, -46.20);
        }

        [TestMethod]
        public void GetDistanceLatLongTest()
        {
            var sCoord = new GeoCoordinate(-23.695784, -46.526691);
            var eCoord = new GeoCoordinate(-23.638512, -46.509942);

            double distance = sCoord.GetDistanceTo(eCoord);
        }

        [TestMethod]
        public void GetPersons()
        {
            JsonSerializer serializer = new JsonSerializer();

            string path = System.IO.Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(string.Format("{0}{1}", path, "\\data\\person.txt")))
            {
                string json = sr.ReadToEnd();
                List<Person> deserializedPerson = JsonConvert.DeserializeObject<List<Person>>(json);
            }
        }

        [TestMethod]
        public void GetCloseFriends()
        {
            Person person = new Person();
            person.nome = "Clever";
            person.nome = "Santoro";
            person.endereco.logradouro = "Rua Caetano Zanela";
            person.endereco.numero = 47;
            person.endereco.estado = "São Paulo";
            person.endereco.cidade = "Santo André";
            person.endereco.pais = "Brasil";

            double lati = 0;
            double longi = 0;
            List<Person> deserializedPerson = new List<Person>();


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

            JsonSerializer serializer = new JsonSerializer();

            string path = System.IO.Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(string.Format("{0}{1}", path, "\\data\\person.txt")))
            {
                string json = sr.ReadToEnd();
                deserializedPerson = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            var Origem = new GeoCoordinate(lati, longi);

            foreach (Person item in deserializedPerson)
            {
                var destino = new GeoCoordinate(item.endereco.latitude, item.endereco.longitude);
                double distance = Origem.GetDistanceTo(destino);
            }

        }

    }
}
