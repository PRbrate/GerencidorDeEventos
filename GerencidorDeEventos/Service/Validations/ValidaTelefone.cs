using System.Text.RegularExpressions;

namespace GerencidorDeEventos.Service.Validations
{
    public class ValidaTelefone
    {
        public static bool ValidarTelefone(string telefone)
        {
            // Expressão regular para formatos comuns (xx) xxxxx-xxxx ou xx xxxxx-xxxx
            string padrao = @"^\(?\d{2}\)?\s?\d{4,5}-\d{4}$";
            return Regex.IsMatch(telefone, padrao);
        }
    }
}
