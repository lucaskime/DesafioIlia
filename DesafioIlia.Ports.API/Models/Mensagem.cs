using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DesafioIlia.Ports.API.Models
{
    public class Mensagem
    {

        [JsonPropertyName("mensagem")]
        public string _Mensagem { get; set; }
    }
}
