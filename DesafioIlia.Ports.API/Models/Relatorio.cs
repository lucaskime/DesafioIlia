using System.Text.Json.Serialization;

namespace DesafioIlia.Ports.API.Models
{
    public class Relatorio
    {
        /// <summary>
        /// Exemplo: 2018-08
        /// </summary>
        /// 
        [JsonPropertyName("mes")]
        public string Mes { get; set; }

        /// <summary>
        /// Exemplo: PT69H35M5S
        /// </summary>
        /// 
        [JsonPropertyName("horasTrabalhadas")]
        public string HorasTrabalhadas { get; set; }

        /// <summary>
        /// Exemplo: PT69H35M5S
        /// </summary>
        /// public string horasExcedentes { get; set; }
        /// 
        [JsonPropertyName("horasDevidas")]
        public string HorasDevidas { get; set; }

        /// <summary>
        /// Exemplo: PT69H35M5S
        /// </summary>
        /// 
        [JsonPropertyName("horasExcedentes")]
        public string HorasExcedentessssssssssss { get; set; }

        /// <summary>
        /// Exemplo: 
        ///     {
        ///         "dia": "2023-04-30",
        ///         "horarios": [
        ///         "08:00:00",
        ///         "12:00:00",
        ///         "13:00:00",
        ///         "18:00:00"
        ///         ]
        ///     }
        /// </summary>
        /// 
        [JsonPropertyName("registros")]
        public List<Registro> Registros { get; set; }
    }
}
