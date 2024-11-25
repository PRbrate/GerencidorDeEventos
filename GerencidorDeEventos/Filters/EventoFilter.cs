namespace GerencidorDeEventos.Filters
{
    public class EventoFilter
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }   
        public string Descricao { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public string Email { get; set; }
        public int NumVagas { get; set; }
        public DateTime DataLimiteInscricao { get; set; }
    }
}
