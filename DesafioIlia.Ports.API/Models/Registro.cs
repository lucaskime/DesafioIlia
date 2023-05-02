using System.Text.Json.Serialization;

namespace DesafioIlia.Ports.API.Models
{
    public class Registro
    {
        /// <summary>
        /// Exemplo: 2018-08
        /// </summary>
        [JsonPropertyName("dia")]
        public string Dia { get; set; }

        /// <summary>
        /// Exemplo: 
        ///         [
        ///         "08:00:00",
        ///         "12:00:00",
        ///         "13:00:00",
        ///         "18:00:00"
        ///         ]
        /// </summary>
        [JsonPropertyName("horarios")]
        public List<string> Horarios { get; set; }
    }
}
