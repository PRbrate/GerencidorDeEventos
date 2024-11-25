namespace GerencidorDeEventos.Filters
{
    public class PalestraFilter
    {
        public int EventoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public string HoraInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string HoraFim { get; set; }
        public string Palestrante { get; set; }
        public string CurriculoPalestrante { get; set; }
    }
}
