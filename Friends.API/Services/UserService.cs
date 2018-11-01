using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Friends.Entities;
using Friends.Helpers;
using Friends.Repository;
using Microsoft.Extensions.Configuration;
using Friends.API.Controllers;

namespace Friends.Services
{
    public interface IUserService
    {
        Usuarios Authenticate(string Usuariosname, string password);
        IEnumerable<Usuarios> GetAll();
    }

    public class UserService : BaseController , IUserService
    {
        // Usuarioss hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Usuarios> _Usuarioss = new List<Usuarios>
        { 
            new Usuarios { Id = 1, Nome = "Test", Sobrenome = "Usuarios", Username = "test", Password = "test" } 
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IConfiguration config)
            :base(config) 
        {
            _appSettings = appSettings.Value;
        }

        public Usuarios Authenticate(string Username, string PassWord)
        {
            Usuarios Usuarios = new Usuarios();
            using (var _uow = new UnitOfWork(_config.GetConnectionString("FriendsBDEntities")))
            {
                Usuarios = _uow.UsuariosRepository.Authenticate(Username, PassWord).Result;
            }

            //var Usuarios = _Usuarioss.SingleOrDefault(x => x.Username == Username && x.Password == password);

            // return null if Usuarios not found
            if (Usuarios == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, Usuarios.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Usuarios.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            Usuarios.Password = null;

            return Usuarios;
        }

        public IEnumerable<Usuarios> GetAll()
        {
            // return Usuarioss without passwords
            return _Usuarioss.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}