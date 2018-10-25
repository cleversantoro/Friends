using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viavarejofriends
{
    public class Address
    {
        public Address() { }

        public string logradouro { get; set; }
        public int numero { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }
}
