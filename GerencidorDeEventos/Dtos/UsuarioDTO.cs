namespace GerencidorDeEventos.Dtos
{
    public class UsuarioDTO
    {
        public UsuarioDTO(int id, string nome, string email, string roles, string cpf)
        {
            Nome = nome;
            Email = email;
            Roles = roles;
            Cpf = cpf;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Cpf { get; set; }

    }
}
