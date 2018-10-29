using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using CopaFilmes.Domain.Entity;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace CopaFilmes.SPA.Controllers
{
    [Route("api/[controller]")]
    public class FilmesController : Controller
    {

        //[HttpGet("[action]")]
        //public async Task<IEnumerable<Filme>> FilmesCopa()
        //{
        //    HttpClient client = new HttpClient();
        //    try
        //    {
        //        string url = "http://copafilmes.azurewebsites.net/api/filmes";
        //        var response = await client.GetStringAsync(url);
        //        var Filmes = JsonConvert.DeserializeObject<List<Filme>>(response);
        //        return Filmes;
        //    }
        //    catch
        //    {
        //        //throw ex;
        //        return null;
        //    }

        //}

        //[HttpPost("GerarCampeonato")]
        //public Final GerarCampeonato([FromBody] List<Filme> filmes)
        //{
        //    Campeonato campeonato = Campeonato.RealizarCampeonato(filmes);
        //    GrupoOitavas oitavas =  campeonato.grupoOitavas;
        //    GrupoQuartas quartas = campeonato.grupoQuartas;
        //    SemiFinal semifinal = campeonato.semiFinal;
        //    Final final= campeonato.final;

        //    return final;
        //}

    }
}
