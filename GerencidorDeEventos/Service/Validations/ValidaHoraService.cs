using System.Globalization;

namespace GerencidorDeEventos.Service.Validations
{
    public static class ValidaHoraService
    {
        public static DateTime AtualizarHora(DateTime data, string novaHora)
        {
            var dataAtualizada = data;

            // Validar e converter a string para DateTime (apenas a hora)
            if (DateTime.TryParseExact(novaHora, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hora))
            {
                // Atualizar a hora no objeto existente
                dataAtualizada = new DateTime(data.Year, data.Month, data.Day, hora.Hour, hora.Minute, hora.Second);
                return dataAtualizada;
            }

            return new DateTime(); // Retorna falso se a string for inválida
        }

        public static bool ValidarHora(string hora)
        {
            return DateTime.TryParseExact(
                hora,
                "HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _ // O resultado convertido é descartado
            );
        }
    }
}
