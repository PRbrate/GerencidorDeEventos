using System.Text.Json.Serialization;

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
        public Usuario(string cpf, string nome, string email, string senha)
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
        public bool Administrador { get; set; } = false;

        [JsonIgnore]
        public List<InscricaoEvento> Inscricoes { get; set; } = new List<InscricaoEvento>();

        [JsonIgnore]
        public ICollection<InscricaoMinicurso> InscricoesMinicurso { get; set; }
        [JsonIgnore]
        public ICollection<InscricaoPalestra> InscricaoPalestras { get; set; }


    }
}
