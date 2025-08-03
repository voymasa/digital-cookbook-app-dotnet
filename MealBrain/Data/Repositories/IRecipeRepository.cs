using MealBrain.Data.Entities;
using MealBrain.Models.DTO;

namespace MealBrain.Data.Repositories
{
    /// <summary>
    /// Add the method stubs for the CRUD operations and necessary db connection and initializers
    /// </summary>
    public interface IRecipeRepository
    {
        string GetStatusMessage();

        //Create
        Task<Ingredient?> InsertIngredient(Ingredient newIngredient);
        Task<Recipe?> InsertRecipe(Recipe newRecipe);
		

		//Read
		Task<List<Recipe>> GetAllRecipesByAcct(Guid? acctGuid);
		Task<Recipe?> GetRecipeByGuid(Guid? guid);
		Task<Recipe?> GetRecipeByName(string name);

		Task<List<Recipe>> GetRecipesByIngredients(List<string> ingredients);
		Task<List<Ingredient>> GetAllIngredients();
		Task<Ingredient?> GetIngredientByGuid(Guid guid);
		Task<Ingredient?> GetIngredientByName(string name);

        //Task<Recipe> GetRecipeByIdAsync(int id);

        //Update
        Task<Recipe?> UpdateRecipe(Recipe updatedRecipe);
        Task<Ingredient?> UpdateIngredient(Ingredient updatedIngredient);

        //Delete
        Task<bool> DeleteRecipeByGuid(Guid guid);
        Task<bool> DeleteIngredientByGuid(Guid guid);
        //Task GetByIdAsync(int id);
    }
}
