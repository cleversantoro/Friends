using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Friends.Domain.Entities 
{
    public partial class Contatos : BaseEntity
    {
        public Contatos()
        {
            endereco = new Enderecos();
        }

        public int id_Endereco { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public Enderecos endereco { get; set; }
    }
}
