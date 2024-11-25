using GerencidorDeEventos.Dtos;

namespace GerencidorDeEventos.Model
{
    public class ProgramacaoEvento
    {
        public int id_evento { get; set; }

        public List<MinicursoDto> minicurso { get; set;} = new List<MinicursoDto>();
        public List<PalestraDto> palestras { get; set; } = new List<PalestraDto>();
    }
}
