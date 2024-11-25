using System.Text.Json.Serialization;

namespace GerencidorDeEventos.Dtos
{
    public class EventoDto
    {
        public EventoDto(string nome, DateTime dt_inicio, DateTime dt_fim,
            string descricao, string nome_responsavel, string cpf_responsavel, 
            string email_responsavel, int numero_vagas, DateTime dt_limite_inscricao)
        {
            this.nome = nome;
            this.dt_inicio = dt_inicio;
            this.dt_fim = dt_fim;
            this.descricao = descricao;
            this.nome_responsavel = nome_responsavel;
            this.cpf_responsavel = cpf_responsavel;
            this.email_responsavel = email_responsavel;
            this.numero_vagas = numero_vagas;
            this.dt_limite_inscricao = dt_limite_inscricao;
        }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dt_inicio { get; set; }
        public DateTime dt_fim { get; set; }
        public string descricao { get; set; }
        public string nome_responsavel { get; set; }
        public string cpf_responsavel { get; set; }
        public string email_responsavel { get; set; }
        public int numero_vagas { get; set; }
        public DateTime dt_limite_inscricao { get; set; }
    }
}
