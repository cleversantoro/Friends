using Xunit;
using Friends.API.Controllers;
using Friends.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Friends.Test
{
    public class ContatosControllerTest
    {

        private ContatosController contatos;
        protected IConfiguration _config { get; private set; }

        public ContatosControllerTest()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appSettings.json");
            _config = configurationBuilder.Build();

            contatos = new ContatosController(_config);
        }

        [Fact]
        public void Teste_Metodo_GetContato_Retorna_Todos_Contatos()
        {
            var okResult = contatos.GetContato();
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Teste_Metodo_GetList_Retorna_Lista_Select()
        {
            var okResult = contatos.GetList();
            Assert.IsType<OkObjectResult>(okResult);
        }


        [Fact]
        public void Teste_Metodo_GetCloseContacts_Retorna_Contatos_proximos_por_id()
        {
            var okResult = contatos.GetCloseContacts(1);
            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}
