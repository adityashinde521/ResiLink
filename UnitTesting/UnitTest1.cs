using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Moq;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {
        private Mock<ICityBll> _mockURepository;
        private CityService _utilService;
        private List<City> _epics;


        [SetUp]
        public void Setup()
        {
            _epics = new List<City>();

            _mockURepository = new Mock<ICityBll>();
            _mockURepository.Setup(r => r.GetAllAsync()).ReturnsAsync(_epics);
            _mockURepository.Setup(r => r.CreateAsync(It.IsAny<City>())).ReturnsAsync((City e) =>
            {
                _epics.Add(e);
                return e;
            });

            _utilService = new CityService(_mockURepository.Object);
        }

        [Test]
        public async Task CreateAysnc_Name_length()
        {
            City city = new City
            {
                Id = Guid.NewGuid(),
                Name="asesezxrdfchgvghfbehfhbbjhbbhjbbfhb",
                Country = "India"
            };

            var ex = Assert.ThrowsAsync<BadRequestException>(() => _utilService.CreateAsync(city));
            Assert.That(ex.Message, Is.EqualTo("City Length is more than 25 characters"));
        }

    }
}