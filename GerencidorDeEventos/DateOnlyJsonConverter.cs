using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerencidorDeEventos
{
    public class DateOnlyJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                // Lê o valor como string e converte para DateTime
                return DateTime.Parse(reader.GetString()!);
            }
            catch (Exception)
            {
                throw new DataExeption("Data Inválida");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Escreve apenas a data no formato "yyyy-MM-dd"
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }

}
