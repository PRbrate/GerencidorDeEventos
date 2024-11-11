using System.Security.Cryptography;


namespace GerencidorDeEventos.Service.Validations
{
    public static class SenhaHashService
    {
        private const int SaltSize = 16; // Tamanho do Salt em bytes
        private const int HashSize = 20; // Tamanho do Hash em bytes
        private const int Iterations = 10000; // Número de iterações do PBKDF2


        public static string SenhaPasword(string senha)
        {
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Combina o salt e o hash em um único array para armazenar
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Retorna o hash em formato Base64
                return Convert.ToBase64String(hashBytes);

            }
        }

        public static bool VerifyPassword(string senha, string senhaHash)
        {
            // Converte o hash armazenado de Base64 para bytes
            byte[] hashBytes = Convert.FromBase64String(senhaHash);

            // Extrai o salt dos bytes do hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Gera o hash da senha fornecida usando o salt extraído
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Compara o hash da senha fornecida com o hash armazenado
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}