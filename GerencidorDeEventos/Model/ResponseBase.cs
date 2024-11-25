namespace GerencidorDeEventos.Model
{
    public class ResponseBase
    {
        public ResponseBase(int id_evento, int id_usuario_participante)
        {
            this.id_evento = id_evento;
            this.id_usuario_participante = id_usuario_participante;
        }

        public int id_evento { get; set; }
        public int id_usuario_participante { get; set; }
    }
}
