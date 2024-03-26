using HotelHub.Controllers;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;

namespace TestProject1 {
    [TestClass]
    public class UnitTest1 {
        
        [TestMethod]
        public void GetHoteis_DeveRetornarOkComListaDeHoteis() {
            // Arrange
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(s => s.GetAllHotels()).Returns(new List<Hotel>());

            var mockHotelHubContext = new Mock<HotelHubContext>();

            var controller = new HotelsController(mockHotelService.Object, mockHotelHubContext.Object);

            // Act
            var result = controller.GetHoteis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetHoteis_DeveRetornarBadRequestSeOcorrerExcecao() {
            // Arrange
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(s => s.GetAllHotels()).Throws(new Exception("Erro ao obter hotéis"));

            var mockHotelHubContext = new Mock<HotelHubContext>();

            var controller = new HotelsController(mockHotelService.Object, mockHotelHubContext.Object);

            // Act
            var result = controller.GetHoteis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostHotel_DeveRetornarOkAoCadastrarHotelComSucesso() {
            // Arrange
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(s => s.PostHotel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Hotel());

            var controller = new HotelsController(mockHotelService.Object, null);

            var admHotelId = 1;
            var nome = "Nome do hotel";
            var descricao = "Descrição do hotel";
            var cidade = "Cidade do hotel";
            var fotos = "Foto1,Foto2,Foto3"; 

            // Act
            var model = new postmodel() {
                admHotelId = admHotelId,
                nome = nome,
                descricao = descricao,
                cidade = cidade,
                fotos = fotos
            };

            // Act
            var result = await controller.PostHotel(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Hotel>)); // Verifique se o resultado é um ActionResult<Hotel>
        }


        [TestMethod]
        public async Task DeleteHotel_DeveRetornarNoContentSeHotelExisteEExclusaoBemSucedida() {
            // Arrange
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(s => s.DeleteHotel(It.IsAny<int>())).ReturnsAsync("ok");

            var mockHotelHubContext = new Mock<HotelHubContext>();

            var controller = new HotelsController(mockHotelService.Object, mockHotelHubContext.Object);

            // Act
            var result = await controller.DeleteHotel(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}