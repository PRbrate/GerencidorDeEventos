namespace GerencidorDeEventos.Model
{
    public class InscricaoEvento : Entity
    {
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Telefone { get; set; }


    }
}
