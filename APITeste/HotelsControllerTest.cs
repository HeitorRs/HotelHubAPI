using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APITeste {
    public class HotelServiceTests : IClassFixture<DbContextFixture> {
        private readonly DbContext _dbContext;

        public HotelServiceTests(DbContextFixture fixture) {
            _dbContext = fixture.DbContext;
        }

        [Fact]
        public void GetAllHotels_ShouldReturnListOfHotels() {
            // Arrange
            var hotelService = new HotelService((HotelHubContext)_dbContext);

            // Act
            var result = hotelService.GetAllHotels();

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions as needed
            }

        }
    }