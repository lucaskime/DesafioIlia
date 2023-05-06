namespace DesafioIlia.Application.Interfaces
{
    public interface IPontoValidation
    {
        Task AdicionarValidateAsync(string diaHora);
        void GerarRelatorioValidate(string anoMes);
    }
}
