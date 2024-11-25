using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;

namespace GerencidorDeEventos.Service.inteface
{
    public interface IMinicursoService
    {
        Task<dynamic> CriarMinicursoService(MinicursoFilter evf);
        Task<dynamic> AtualizaMinicursoService(int id, MinicursoFilter evf);
        Task<dynamic> DeletarMinicursoService(int id);
        Task<dynamic> GetMinicursoPorId(int id);
        Task<List<MinicursoDto>> GetMinicursos();
    }
}
