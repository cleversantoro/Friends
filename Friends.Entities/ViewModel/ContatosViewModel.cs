using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.Entities
{
    public class ContatosViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Distancia { get; set; }
    }
}
