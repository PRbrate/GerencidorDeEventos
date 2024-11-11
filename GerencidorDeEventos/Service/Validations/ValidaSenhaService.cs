using System.Text.RegularExpressions;

namespace GerencidorDeEventos.Service.Validations
{
    public static class ValidaSenhaService
    {
        public static bool VerificarSenha(string senha)
        {
            // Expressão regular para verificar os requisitos da senha
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(senha, pattern);
        }
    }
}
