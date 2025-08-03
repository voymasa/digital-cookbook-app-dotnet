using MealBrain.Data.Entities;

using SQLite;

using System;
using System.Diagnostics;
using System.Linq.Expressions;

using System.Threading.Tasks;
using System.Xml.Linq;
using MealBrain.Models.DTO;

namespace MealBrain.Data.Repositories
{

	public class RecipeRepository : BaseRepository, IRecipeRepository
	{
		public RecipeRepository(string dbPath) : base(dbPath)
		{
		}

		protected override async Task Init()
		{
			// if there is already a connection, make sure the tables are already created, then make sure it has the updated records it should
			if (_connection is not null)
			{
				return;
			}

			_connection = new SQLite.SQLiteAsyncConnection(_dbPath);
			await _connection.CreateTablesAsync<Recipe, Ingredient>();
			await InitializeTables();
		}

		protected override async Task InitializeTables()
		{
			// declare variables for defaults
			Recipe? defRecipe = null;
			Ingredient? defIngredient = null;

			var currentRecipes = await _connection.Table<Recipe>().ToListAsync();
			if (currentRecipes.Count == 0)
			{
				defRecipe = new Recipe
				{
					Guid = Guid.NewGuid(),
					RecipeName = "Sugar Cookies",
				};
				await _connection.InsertAsync(defRecipe);
			}

			var currentIngredients = await _connection.Table<Ingredient>().ToListAsync();
			if (currentIngredients.Count == 0)
			{
				defIngredient = new Ingredient
				{
					Guid = Guid.NewGuid(),
					RecipeName = "Granulated Sugar"
				};
				await _connection.InsertAsync(defIngredient);
			}

			if (defRecipe != null && defIngredient != null)
			{
				defRecipe.Ingredients.Add(defIngredient);
				defIngredient.Recipes.Add(defRecipe);
				await _connection.UpdateAsync(defRecipe);
				await _connection.UpdateAsync(defIngredient);
			}
		}
		public async Task<bool> DeleteIngredientByGuid(Guid guid)
		{
			await Init();
			var ingredient = await _connection.FindAsync<Ingredient>(guid);
			if (ingredient == null)
				return false;
			await _connection.DeleteAsync(ingredient);

			return true;
		}

		public async Task<bool> DeleteRecipeByGuid(Guid guid)
		{
			await Init();
			var recipe = await _connection.FindAsync<Recipe>(guid);
			if (recipe == null)
				return false;
			await _connection.DeleteAsync(recipe);

			return true;
		}

		public async Task<List<Ingredient>> GetAllIngredients()
		{
			await Init();
			return await _connection.Table<Ingredient>().ToListAsync();
		}

		public async Task<List<Recipe>> GetAllRecipesByAcct(Guid? acctGuid)
		{
			await Init();

            return await _connection.Table<Recipe>().Where(u => u.AccountGuid == acctGuid).ToListAsync();

		}

		public async Task<Ingredient?> GetIngredientByGuid(Guid guid)
		{
			await Init();
			return await _connection.FindAsync<Ingredient>(guid);
		}

		public async Task<Ingredient?> GetIngredientByName(string name)
		{
			await Init();
			return await _connection.Table<Ingredient>()
				.Where(i => i.IngredientName == name).FirstOrDefaultAsync();

		}

		public async Task<Recipe?> GetRecipeByGuid(Guid? guid)
		{
			await Init();
			return await _connection.FindAsync<Recipe>(guid);

		}
		public async Task<List<Recipe>> GetRecipesByIngredients(List<string> ingredients)
		{
			await Init();
			var matchIngredients = await _connection.Table<Ingredient>()
			  .Where(i => ingredients.Contains(i.IngredientName)).ToListAsync();

			if (matchIngredients.Count != ingredients.Count)
				return null;

			var recipeGuids = matchIngredients.Select(i => i.Guid).ToList();

			var recipes = await _connection.Table<RecipeToIngredient>()
				.Where(rti => recipeGuids.Contains(rti.IngredientGuid)).ToListAsync();

			var group = recipes.GroupBy(rti => rti.RecipeGuid)
				.FirstOrDefault(g => g.Select(x => x.IngredientGuid).Distinct()
				.Count() == recipeGuids.Count);

			if (group == null) return null;

			return await _connection.FindAsync<List<Recipe>>(group.Key);
		}

		public async Task<Recipe?> GetRecipeByName(string name)
		{
			await Init();
			return await _connection.Table<Recipe>()
				.Where(i => i.RecipeName == name).FirstOrDefaultAsync();
		}

		public async Task<Recipe?> InsertRecipe(Recipe newRecipe)
		{
			await Init();
			var result = await _connection.InsertAsync(newRecipe);
			return result > 0 ? newRecipe : null;

		}

		public async Task<Ingredient?> InsertIngredient(Ingredient newIngredient)
		{
			await Init();
			var existing = await _connection.Table<Ingredient>()
				.Where(i => i.IngredientName == newIngredient.IngredientName).FirstOrDefaultAsync();
			if (existing != null) return existing;

			newIngredient.Guid = Guid.NewGuid();
			var result = await _connection.InsertAsync(newIngredient);
			return result > 0 ? newIngredient : null;
		}

		public async Task<Ingredient?> UpdateIngredient(Ingredient updatedIngredient)
		{
			await Init();
			var existing = await _connection.FindAsync<Ingredient>(updatedIngredient.Guid);
			if (existing == null) return null;

			var result = await _connection.UpdateAsync(updatedIngredient);
			return result > 0 ? updatedIngredient : null;
		}

		public async Task<Recipe?> UpdateRecipe(Recipe updatedRecipe)
		{
			await Init();
			var result = await _connection.UpdateAsync(updatedRecipe);
			return result > 0 ? updatedRecipe : null;
		}
		protected override async Task MigrateTables()
		{
			// Todo: consider how we want to implement migration changes to the default table information
		}
	}
}
