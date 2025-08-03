
using MealBrain.Data.Entities;
using MealBrain.Data.Repositories;
using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;

namespace MealBrain.Utilities.Services
{
    /// <summary>
    /// The Recipe service is an abstraction to broker between the MVVM model and the repositor(y/ies).
    /// This class should handle conversion between the model(s) and the entit(y/ies), making calls to the
    /// repositories, and packaging the data for transferring back to the model.
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<CrudResult<List<RecipeDTO>>> GetAllRecipesForAcct(Guid? acctGuid)
        {
            var queryResult = new CrudResult<List<RecipeDTO>>();

            var result = await _recipeRepository.GetAllRecipesByAcct(acctGuid);

            queryResult.IsSuccess = true;
            queryResult.ResultMessage = _recipeRepository.GetStatusMessage();
            queryResult.Data = MapEntitiesToDtos(result);

            return queryResult;
        }

        public async Task<Utilities.ServiceResults.CrudResult<RecipeDTO>> GetRecipeByIdAsync(Guid? id)
        {
            var recipe = await _recipeRepository.GetRecipeByGuid(id);
            if (recipe == null)
                return Utilities.ServiceResults.CrudResult<RecipeDTO>.Fail("Recipe not found");

            var dto = MapEntityToDto(recipe);


            return Utilities.ServiceResults.CrudResult<RecipeDTO>.Ok(dto);
        }

        private RecipeDTO MapEntityToDto(Data.Entities.Recipe entity)
        {
            var dto = new RecipeDTO
            {
                Guid = entity.Guid,
                RecipeName = entity.RecipeName,
                Description = entity.Description,
                PreparationTime = entity.PreparationTime,
                Servings = entity.Servings,
                CookingTime = entity.CookingTime,
                ImageUrl = entity.ImageUrl,


                Ingredient = entity.Ingredients
            ?.Select(ri => ri.IngredientName ?? string.Empty)
            .ToList() ?? new List<string>(),

                RecipeInstruction = string.Join("\n", entity.RecipeInstruction
            ?.OrderBy(i => i.StepNumber)
            .Select(i => i.Instruction) ?? new List<string>())
            };

            return dto;
        }

        /// <summary>
        /// This method is to convert from the data account data transfer object to
        /// an entity object to use with the database.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private Data.Entities.Recipe MapDtoToEntity(RecipeDTO dto)
        {
            var entity = new Data.Entities.Recipe
            {
                Guid = dto.Guid ?? Guid.NewGuid(),
                RecipeName = dto.RecipeName,
                Description = dto.Description,
                PreparationTime = dto.PreparationTime,
                CookingTime = dto.CookingTime,
                ImageUrl = dto.ImageUrl,
            };

            return entity;
        }

        /// <summary>
        /// This method converts a list of account entities into account dtos
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        private List<RecipeDTO> MapEntitiesToDtos(List<Data.Entities.Recipe> entities)
        {
            List<RecipeDTO> dtos = new List<RecipeDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(MapEntityToDto(entity));
            }

            return dtos;
        }

        public async Task<bool> InsertRecipeAsync(Recipe recipe)
        {
            var result = await _recipeRepository.InsertRecipe(recipe);
            return result != null;
        }

        public async Task<bool> UpdateRecipeAsync(Recipe recipe)
        {
            var result = await _recipeRepository.UpdateRecipe(recipe);
            return result != null;
        }

        public async Task<bool> DeleteRecipeAsync(Guid recipeGuid)
        {
            return await _recipeRepository.DeleteRecipeByGuid(recipeGuid);
        }
    }
}

