﻿using Friends.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Repository.Interface
{
    public interface IContatosRepository
    {
        Task<Contatos> GetByID(int id);
        Task<IEnumerable<Contatos>> SelectAll();
        Task<IEnumerable<ContatosListViewModel>> SelectList();
        List<ContatosViewModel> GetCloseContacts(int id);
    }
}

