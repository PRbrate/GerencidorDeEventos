namespace GerencidorDeEventos.Model
{
    public class InscricaoMinicurso
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int MinicursoId { get; set; }
        public Minicurso Minicurso { get; set; }

        public string Telefone {  get; set; }
        public string email { get; set; }
    }
}
