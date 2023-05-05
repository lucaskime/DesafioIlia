using DesafioIlia.Application.DTOs;
using System.Text.Json.Serialization;

namespace DesafioIlia.Ports.API.Models
{
    public class Momento
    {
        /// <summary>
        /// Exemplo: "2018-08-22T08:00:00"
        /// </summary>
        [JsonPropertyName("dataHora")]
        public string DataHora { get; set; }

        internal MomentoDTO ToDTO()
        {
            var dto = new MomentoDTO();
            dto.DataHora = DataHora;
            return dto;
        }
    }
}
