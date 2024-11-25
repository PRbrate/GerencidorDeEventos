using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;

namespace GerencidorDeEventos.Service.inteface
{
    public interface IPalestraService
    {
        Task<dynamic> CriarPalestraService(PalestraFilter evf);
        Task<dynamic> AtualizaPalestraService(int id, PalestraFilter evf);
        Task<dynamic> DeletarPalestraService(int id);
        Task<List<PalestraDto>> GetPalestras();
    }
}
