namespace GerencidorDeEventos.Model
{
    public class Usuario : Entity
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; }
    }
}
