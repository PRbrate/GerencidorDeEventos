using System.Text.RegularExpressions;

namespace GerencidorDeEventos.Service.Validations
{
    public class ValidaEmailService
    {
        public static bool VerificaEmail(string email)
        {
            // Expressão regular para verificar os requisitos da senha
            string pattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
