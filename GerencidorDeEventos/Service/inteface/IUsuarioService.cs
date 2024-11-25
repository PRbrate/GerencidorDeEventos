using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerencidorDeEventos.Service.inteface
{
    public interface IUsuarioService
    {
        Task<dynamic> UsuarioAuthenticatorService(string cpf, string senha);
        Task<List<Usuario>> GetTodosUsuariosService();
        Task<dynamic> CriarUsuarioService(UsuarioFilter usuarioFilter);
        Task<dynamic> AtualizarUsuarioService(UsuarioFilter usuarioFilter, string token, int id);
        Task<Usuario> GetUsuarioByIdService(int id);
        Task<dynamic> DeleteUsuarioService(int id_usuario);
        Task<dynamic> PromoveUsuarioService(int id);
    }
}
