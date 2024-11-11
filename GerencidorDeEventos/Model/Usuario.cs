namespace GerencidorDeEventos.Model
{
    public class Usuario : Entity
    {

        public Usuario() { }
        public Usuario(string email, string cpf) 
        {
           Email = email;
           Cpf = cpf;
        }
        public Usuario(string cpf, string nome, string email, string senha, string roles)
        {
            Cpf = cpf;
            Nome = nome;
            Email = email;
            Senha = senha;
            Roles = roles;
        }

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; } = "usuario";
    }
}
