using MealBrain.Data.Entities;
using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;

namespace MealBrain.Utilities.Services
{
    /// <summary>
    /// Put method stubs here that should be implemented in the concrete class related to brokering between the MVVM model and the data layer
    /// </summary>
    public interface IRecipeService
    {
        Task<Utilities.ServiceResults.CrudResult<RecipeDTO>> GetRecipeByIdAsync(Guid? id);
        Task<CrudResult<List<RecipeDTO>>> GetAllRecipesForAcct(Guid? acctGuid);

        Task<bool> DeleteRecipeAsync(Guid recipeGuid);
        Task<bool> InsertRecipeAsync(Recipe recipe);
        Task<bool> UpdateRecipeAsync(Recipe recipe);
    }
}
