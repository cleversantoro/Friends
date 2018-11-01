using Friends.Domain.Entities;
using Friends.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Repository.Interface
{
    public interface IUsuariosRepository
    {
        Task<Usuarios> Authenticate(string Username, string password);
    }
}

