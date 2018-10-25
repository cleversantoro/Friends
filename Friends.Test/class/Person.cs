using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace viavarejofriends
{
    public class Person
    {
        public Person()
        {
            endereco = new Address(); 
        }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public Address endereco { get; set; }
    }
}
