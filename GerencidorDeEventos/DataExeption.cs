namespace GerencidorDeEventos
{
    public class DataExeption : Exception
    {
        public DataExeption()
        : base("Data inválida") { }
        public DataExeption(string mensagem)
            : base(mensagem) { }

    }
}
