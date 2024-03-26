using Moq;

namespace TestHotelHub {
    [TestClass]
    public class HotelsControllerTest {
        [TestMethod]
        public void GetHoteis_DeveRetornarOkComListaDeHoteis() {
            // Arrange
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(s => s.GetAllHotels()).Returns(new List<Hotel>());

            var controller = new HotelsController(mockHotelService.Object);

            // Act
            var result = controller.GetHoteis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetHoteis_DeveRetornarBadRequestSeOcorrerExcecao() {
            // Arrange
            var mockHotelService = new Mock<IHotelService>();
            mockHotelService.Setup(s => s.GetAllHotels()).Throws(new Exception("Erro ao obter hotéis"));

            var controller = new HotelsController(mockHotelService.Object);

            // Act
            var result = controller.GetHoteis();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}