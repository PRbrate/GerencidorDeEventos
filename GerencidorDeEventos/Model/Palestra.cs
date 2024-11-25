namespace GerencidorDeEventos.Model
{
    public class Palestra : Entity
    {

        public Palestra(int eventoId, string nome, string descricao, DateTime dataInicio, DateTime dataFim, string palestrante, string curriculoPalestrante)
        {
            EventoId = eventoId;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Palestrante = palestrante;
            CurriculoPalestrante = curriculoPalestrante;
        }

        public int EventoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Palestrante { get; set; }
        public string CurriculoPalestrante { get; set; }
        public Evento Evento { get; set; }
        public ICollection<InscricaoPalestra> Inscricoes { get; set; }
    }
}
