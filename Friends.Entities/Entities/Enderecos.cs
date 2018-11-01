using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Domain.Entities
{
    public partial class Enderecos : BaseEntity
    {
        public Enderecos() { }

        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
