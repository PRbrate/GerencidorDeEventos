namespace GerencidorDeEventos.Model
{
    public class Minicurso : Entity
    {
        public Minicurso(int eventoId, string nome, string descricao, DateTime dataInicio, DateTime dataFim, string palestrante, string curriculoPalestrante, int quantidadeDeVagas, DateTime limiteInscricao)
        {
            EventoId = eventoId;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Palestrante = palestrante;
            CurriculoPalestrante = curriculoPalestrante;
            QuantidadeDeVagas = quantidadeDeVagas;
            LimiteInscricao = limiteInscricao;
        }

        public int EventoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Palestrante { get; set; }
        public string CurriculoPalestrante { get; set; }
        public int QuantidadeDeVagas { get; set; }
        public DateTime LimiteInscricao { get; set; }
        public Evento Evento { get; set; }
        public ICollection<InscricaoMinicurso> Inscricoes { get; set; }



    }
}
