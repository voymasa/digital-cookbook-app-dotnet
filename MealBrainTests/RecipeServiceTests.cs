using Xunit;
using Moq;
using MealBrain.Services;
using MealBrain.Data.Repositories;
using MealBrain.Data.Entities;
using MealBrain.DTOs;
using MealBrain.Utilities;
using System.Threading.Tasks;

public class RecipeServiceTests
{
    [Fact]
    public async Task GetRecipeByIdAsync_ReturnsRecipe_WhenFound()
    {
        // Arrange
        var mockRepo = new Mock<IRecipeRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Recipe { Id = 1, Name = "Test" });
        var service = new RecipeService(mockRepo.Object);

        // Act
        var result = await service.GetRecipeByIdAsync(1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal("Test", result.Data.Name);
    }
}