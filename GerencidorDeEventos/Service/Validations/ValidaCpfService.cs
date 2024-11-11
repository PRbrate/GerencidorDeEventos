namespace GerencidorDeEventos.Service.Validations
{
    using System;
    using System.Linq;

    public static class ValidaCpfService
    {
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem exatamente 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais (ex. 111.111.111-11)
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);

            int primeiroDigitoVerificador = soma % 11;
            primeiroDigitoVerificador = primeiroDigitoVerificador < 2 ? 0 : 11 - primeiroDigitoVerificador;

            // Verifica o primeiro dígito
            if (cpf[9] - '0' != primeiroDigitoVerificador)
                return false;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);

            int segundoDigitoVerificador = soma % 11;
            segundoDigitoVerificador = segundoDigitoVerificador < 2 ? 0 : 11 - segundoDigitoVerificador;

            // Verifica o segundo dígito
            return cpf[10] - '0' == segundoDigitoVerificador;
        }
    }

}
