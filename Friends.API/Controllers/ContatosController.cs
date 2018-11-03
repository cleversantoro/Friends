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

        [HttpGet("GetContatos")]
        public IActionResult GetContato()
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.SelectAll();
                return Ok(m.Result);
            }
        }

        [HttpGet("GetList")]
        public IActionResult GetList()
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.SelectList();
                return Ok(m.Result);
            }
        }

        [HttpPost("GetCloseContacts")]
        public IActionResult GetCloseContacts([FromBody] Usuarios userParam)
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.GetCloseContacts(userParam.Nome,userParam.Sobrenome);
                return Ok(m.ToList());
            }
        }

        [HttpGet("GetCloseContacts/{id}")]
        public IActionResult GetCloseContacts(int id)
        {
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                var m = _uow.ContatosRepository.GetCloseContacts(id);
                return Ok(m.ToList());
            }
        }
    }
}