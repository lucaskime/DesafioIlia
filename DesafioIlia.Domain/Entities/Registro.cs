namespace DesafioIlia.Domain.Entities
{
    public sealed class Registro
    {
        public int Id { get; private set; }

        public DateTime DiaHora { get; private set; }

        public Registro(DateTime diaHora)
        {
            DiaHora = diaHora;
        }
    }
}
