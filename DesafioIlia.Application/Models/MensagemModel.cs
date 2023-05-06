using System.Text.Json.Serialization;

namespace DesafioIlia.Application.Models
{
    public class MensagemModel
    {
        public string Mensagem { get; private set; }
        public MensagemModel(string message)
        {
            Mensagem = message;
        }
    }
}
