using Friends.Domain.Entities;

namespace Friends.Entities
{
    public class Usuarios : BaseEntity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}