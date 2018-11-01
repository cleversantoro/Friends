using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friends.Entities;
using Friends.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Friends.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ContatosController : BaseController
    {

        public ContatosController(IConfiguration config)
            :base(config)
        {
            
        }

        //[AllowAnonymous]
        [HttpGet("GetContatos")]
        public IActionResult GetContato()
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.SelectAll();
                return Ok(m.Result);
            }
        }

        //[AllowAnonymous]
        [HttpPost("GetCloseContacts")]
        public IActionResult GetCloseContacts([FromBody] Usuarios userParam)
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.GetCloseContacts(userParam.Nome,userParam.Sobrenome);
                return Ok(m.ToList());
            }
        }
    }
}