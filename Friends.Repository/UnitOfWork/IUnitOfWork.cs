using Friends.Repository.Interface;
using System;

namespace Friends.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IContatosRepository ContatosRepository { get; }
        IUsuariosRepository UsuariosRepository { get; }

        void Commit();
    }
}
