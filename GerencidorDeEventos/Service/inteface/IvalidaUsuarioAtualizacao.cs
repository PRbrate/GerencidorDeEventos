namespace GerencidorDeEventos.Service.inteface
{
    public interface IvalidaUsuarioAtualizacao
    {
        bool validaEmailCpfToken(string email, string cpf);

    }
}
