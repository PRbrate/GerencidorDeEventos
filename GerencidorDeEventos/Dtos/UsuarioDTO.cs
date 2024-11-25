namespace GerencidorDeEventos.Dtos
{
    public class UsuarioDTO
    {
        public UsuarioDTO(string cpf, string nome, string email, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }



    }
}
