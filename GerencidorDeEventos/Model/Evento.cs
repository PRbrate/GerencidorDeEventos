namespace GerencidorDeEventos.Model
{
    public class Evento : Entity
    {
        public Evento(string nome, DateTime dataInicio, DateTime dataFim, string descricao,
            string nomeResponsavel, string cpfResponsavel, string email, int numVagas,
            DateTime dataLimiteInscricao)
        {
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Descricao = descricao;
            NomeResponsavel = nomeResponsavel;
            CpfResponsavel = cpfResponsavel;
            Email = email;
            NumVagas = numVagas;
            DataLimiteInscricao = dataLimiteInscricao;
        }

        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Descricao { get; set; }
        public string  NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public string Email { get; set; }
        public int NumVagas { get; set; }
        public DateTime DataLimiteInscricao { get; set; }
        public List<InscricaoEvento> Inscricoes { get; set; } = new List<InscricaoEvento>();
        public ICollection<Minicurso> Minicursos { get; set; }
        public ICollection<Palestra> Palestras { get; set;}


    }
}
