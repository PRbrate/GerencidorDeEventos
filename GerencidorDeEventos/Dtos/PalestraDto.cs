using System.Text.Json.Serialization;

namespace GerencidorDeEventos.Dtos
{
    public class PalestraDto
    {
        public PalestraDto(int id_evento, string nome, string descricao, string dt_minicurso, string hora_inicio_minicurso, string hora_fim_minicurso, string nome_instrutor, string minicurriculo_instrutor)
        {
            this.id_evento = id_evento;
            this.nome = nome;
            this.descricao = descricao;
            this.dt_minicurso = dt_minicurso;
            this.hora_inicio_minicurso = hora_inicio_minicurso;
            this.hora_fim_minicurso = hora_fim_minicurso;
            this.nome_instrutor = nome_instrutor;
            this.minicurriculo_instrutor = minicurriculo_instrutor;
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
    }
}
