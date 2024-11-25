using System.Text.RegularExpressions;

namespace GerencidorDeEventos.Service.Validations
{
    public static class ValidaSenhaService
    {
        public static bool VerificarSenha(string senha)
        {
            
            if (senha.Length < 8)
                return false;

            if (!Regex.IsMatch(senha, @"[A-Z]"))
                return false;
            if (!Regex.IsMatch(senha, @"[a-z]"))
                return false;
            if (!Regex.IsMatch(senha, @"[\W_]"))
                return false;

            return true;
        }
    }
}
