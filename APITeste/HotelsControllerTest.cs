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

            // Simulando uma lista de hot�is
            var expectedHotels = new List<Hotel>
            {
            new Hotel { HotelId = 1, Nome = "Hotel A", Descricao = "Descri��o do Hotel A", Cidade = "Cidade A" },
            new Hotel { HotelId = 2, Nome = "Hotel B", Descricao = "Descri��o do Hotel B", Cidade = "Cidade B" },
            // Adicione mais hot�is conforme necess�rio para o teste
        };

            // Definindo o comportamento esperado do servi�o ao chamar o m�todo GetAllHotels
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
            var expectedHotel = new Hotel { HotelId = hotelId, Nome = "Hotel A", Descricao = "Descri��o do Hotel A", Cidade = "Cidade A" };
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