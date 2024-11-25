namespace GerencidorDeEventos.Model
{
    public class InscricaoPalestra
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int PalestraId { get; set; }
        public Palestra Palestra { get; set; }

        public string Telefone { get; set; }
        public string email { get; set; }

    }
}
