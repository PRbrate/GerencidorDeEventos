using System.Text.Json.Serialization;

namespace GerencidorDeEventos.Dtos
{
    public class MinicursoDto
    {
        public MinicursoDto(int id_evento, string nome, string descricao, string dt_minicurso, string hora_inicio_minicurso, string hora_fim_minicurso, string nome_instrutor, string minicurriculo_instrutor, int numero_vagas, string dt_limite_inscricao)
        {
            this.id_evento = id_evento;
            this.nome = nome;
            this.descricao = descricao;
            this.dt_minicurso = dt_minicurso;
            this.hora_inicio_minicurso = hora_inicio_minicurso;
            this.hora_fim_minicurso = hora_fim_minicurso;
            this.nome_instrutor = nome_instrutor;
            this.minicurriculo_instrutor = minicurriculo_instrutor;
            this.numero_vagas = numero_vagas;
            this.dt_limite_inscricao = dt_limite_inscricao;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? id_evento { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string dt_minicurso { get; set; }
        public string hora_inicio_minicurso { get; set; }
        public string hora_fim_minicurso { get; set; }
        public string nome_instrutor { get; set; }
        public string minicurriculo_instrutor { get; set; }
        public int numero_vagas { get; set; }
        public string dt_limite_inscricao { get; set; }
    }
}
