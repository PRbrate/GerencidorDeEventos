namespace GerencidorDeEventos.Model
{
    public class ErroMessage
    {
        public ErroMessage(string message) 
        {
            Error = true;
            Message = message;
        }
        public bool Error { get; set; }
        public string Message { get; set; }


    }
}

