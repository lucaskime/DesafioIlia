using DesafioIlia.Application.Interfaces;
using DesafioIlia.Application.Models;
using DesafioIlia.Application.Services;
using DesafioIlia.Domain.Entities;
using DesafioIlia.Domain.Interface;
using Moq;

namespace DesafioIlia.Tests
{
    public class PontoServiceTests
    {
        [Fact]
        public async Task AdicionarAsync_ValidMoment_ReturnsRegistroModel()
        {
            //Arrange
            var momento = "2023-05-06T09:00:00";
            var registro = new Registro(DateTime.Parse(momento));
            var registroModelExpected = new RegistroModel()
            {
                dia = registro.DiaHora.ToString("yyyy-MM"),
                horarios = new List<string>() { registro.DiaHora.ToString("HH:mm:ss") }
            };

            var pontoRepositoryMock = new Mock<IPontoRepository>();
            pontoRepositoryMock.Setup(r => r.AdicionarAsync(registro))
                               .Returns(Task.CompletedTask);

            pontoRepositoryMock.Setup(r => r.ConsultarAsync(registro.DiaHora.Year, registro.DiaHora.Month, registro.DiaHora.Day))
                               .ReturnsAsync(new List<Registro>() { registro });

            var pontoValidationMock = new Mock<IPontoValidation>();
            pontoValidationMock.Setup(v => v.AdicionarValidateAsync(momento))
                               .Returns(Task.CompletedTask);

            var pontoService = new PontoService(pontoRepositoryMock.Object, pontoValidationMock.Object);

            //Act
            var result = await pontoService.AdicionarAsync(momento);

            //Assert
            Assert.Equal(registroModelExpected.dia, result.dia);
            Assert.Equal(registroModelExpected.horarios, result.horarios);
        }
    }

}
