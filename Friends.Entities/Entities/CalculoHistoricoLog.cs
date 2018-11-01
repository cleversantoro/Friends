using Friends.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.Entities
{
    public class CalculoHistoricoLog : BaseEntity
    {
        public int id_Contato { get; set; }
        public int id_Amigo { get; set; }
        public string Distancia { get; set; }

    }
}
