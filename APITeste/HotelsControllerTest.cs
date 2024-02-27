using HotelHub.Controllers;
using HotelHub.Models;
using HotelHub.Services;
using Intuit.Ipp.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APITeste {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void TestarPegarHoteis() {

            // Arrange
            var mockHotelService = new Mock<HotelService>();

            // Simulando uma lista de hotéis
            var expectedHotels = new List<Hotel>
            {
            new Hotel { HotelId = 1, Nome = "Hotel A", Descricao = "Descrição do Hotel A", Cidade = "Cidade A" },
            new Hotel { HotelId = 2, Nome = "Hotel B", Descricao = "Descrição do Hotel B", Cidade = "Cidade B" },
            // Adicione mais hotéis conforme necessário para o teste
        };

            // Definindo o comportamento esperado do serviço ao chamar o método GetAllHotels
            mockHotelService.Setup(service => service.GetAllHotels()).Returns(expectedHotels);

            var hotelController = new HotelsController(mockHotelService.Object);

            // Act
            var result = hotelController.GetHoteis();

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var hotels = okResult.Value as IEnumerable<Hotel>;
            Assert.IsNotNull(hotels);
        }

        [TestMethod]
        public void TestarPegarHotelPerId() {

            int hotelId = 1;
            var expectedHotel = new Hotel { HotelId = hotelId, Nome = "Hotel A", Descricao = "Descrição do Hotel A", Cidade = "Cidade A" };
            var mockHotelService = new Mock<HotelService>();
            mockHotelService.Setup(service => service.GetHotelPerId(hotelId)).Returns(expectedHotel);
            var hotelController = new HotelsController(mockHotelService.Object);


            var result = hotelController.GetHotel(hotelId);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var hotel = okResult.Value as Hotel;
            Assert.IsNotNull(hotel);
            Assert.AreEqual(expectedHotel.HotelId, hotel.HotelId);
        }
    }
    
}