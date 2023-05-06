using DesafioIlia.Application.Interfaces;
using DesafioIlia.Application.Models;
using DesafioIlia.Ports.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioIlia.Tests
{
    public class PontoControllerTests
    {
        private readonly Mock<IPontoService> _mockPontoService;
        private readonly PontoController _controller;

        public PontoControllerTests()
        {
            _mockPontoService = new Mock<IPontoService>();
            _controller = new PontoController(_mockPontoService.Object);
        }

        [Fact]
        public async Task InsereBatida_ReturnsCreated()
        {
            // Arrange
            var momento = new MomentoModel { dataHora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") };
            var registroModel = new RegistroModel {dia = DateTime.Now.ToString("yyyy-MM"), horarios = new List<string>() { "08:00:00", "12:00:00" } };
            _mockPontoService.Setup(p => p.AdicionarAsync(momento.dataHora)).ReturnsAsync(registroModel);

            // Act
            var result = await _controller.insereBatida(momento) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            var returnedModel = Assert.IsType<RegistroModel>(result.Value);
            Assert.Equal(registroModel.dia, returnedModel.dia);
        }

    }

}
