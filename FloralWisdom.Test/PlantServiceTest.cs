using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.ViewModels;
using FloralWisdom.Data.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace FloralWisdom.Tests
{
	public class PlantServiceTests
	{
		[Fact]
		public async Task CreatePlantAsync_AddsPlantToRepository()
		{
			// Arrange
			var mockRepo = new Mock<IRepository<Plant, string>>();
			var plantService = new PlantService(mockRepo.Object);

			var viewModel = new PlantViewModel
			{
				Name = "Rose",
				ScientificName = "Rosa",
				Description = "A beautiful flower.",
				SunlightRequirement = "Full Sun",
				WateringFrequency = 3
			};

			Plant? capturedPlant = null;
			mockRepo
				.Setup(r => r.AddAsync(It.IsAny<Plant>()))
				.Callback<Plant>(plant => capturedPlant = plant)
				.Returns(Task.CompletedTask);

			// Act
			await plantService.CreatePlantAsync(viewModel);

			// Assert
			mockRepo.Verify(r => r.AddAsync(It.IsAny<Plant>()), Times.Once);
			Assert.NotNull(capturedPlant);
			Assert.Equal("Rose", capturedPlant!.Name);
			Assert.Equal("Rosa", capturedPlant.ScientificName);
			Assert.Equal("A beautiful flower.", capturedPlant.Description);
			Assert.Equal("Full Sun", capturedPlant.SunlightRequirement);
			Assert.Equal(3, capturedPlant.WateringFrequency);
		}
	}
}
