using MealBrain.Data.Entities;
using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;
using MealBrain.Utilities.Services;
using SQLite;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MealBrain.Models
{
    [Preserve(AllMembers = true)]
    public class RecipeDetailModel
    {
        private readonly IRecipeService _recipeService;
        private readonly IMeasurementService _measurementService;


        public RecipeDetailModel(IRecipeService recipeService, IMeasurementService measurementService)
        {
            _recipeService = recipeService;
            _measurementService = measurementService;
        }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string RecipeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new(); // Text tags from user

        public ObservableCollection<IngredientEntry> Ingredients { get; set; } = new();
        public ObservableCollection<string> Instructions { get; set; } = new();
        public string NewInstruction { get; set; } = string.Empty;
        public ObservableCollection<MeasurementDTO> Measurements { get; set; } = [];

        public IngredientEntry NewIngredient { get; set; } = new();

        public int PrepTime { get; set; } = 0;
        public int CookTime { get; set; } = 0;
        public int Servings { get; set; } = 1;

        public bool IsNew { get; set; } = true; // True if creating a new recipe
        public bool WasSuccess { get; set; }
        public string NotificationMessage { get; set; } = string.Empty;



        public async Task LoadMeasurementList()
        {
            CrudResult<List<MeasurementDTO>> result = await _measurementService.GetMeasurementsList();
            NotificationMessage = result.ResultMessage;
            WasSuccess = result.IsSuccess;

            if (WasSuccess)
            {
                Measurements = result.Data?.ToObservableCollection<MeasurementDTO>();
            }
        }
        public async Task AddInstruction()
        {
            Instructions.Add(NewInstruction.Trim());
            NewInstruction = string.Empty;
        }

        public async Task AddIngredient()
        {
            Ingredients.Add(NewIngredient);
            NewIngredient = new();
        }

        public async Task SaveAsync(ISessionContext session)
        {
            if (session.CurrentAccount == null) return;

            var entity = MapToEntity(session);

            if (IsNew)
            {
                await _recipeService.InsertRecipeAsync(entity);
                
            }
            else
                await _recipeService.UpdateRecipeAsync(entity);
                Shell.Current.GoToAsync("..");
        }

        public async Task DeleteAsync()
        {
            await _recipeService.DeleteRecipeAsync(Guid);
        }

        public async Task LoadAsync(Guid recipeId)
        {
            await LoadMeasurementList();
            var result = await _recipeService.GetRecipeByIdAsync(recipeId);
            if (!result.IsSuccess || result.Data == null)
                return;

            var dto = result.Data;

            // Map DTO back into model
            this.Guid = dto.Guid ?? Guid.NewGuid();
            this.RecipeName = dto.RecipeName;
            this.Description = dto.Description;
            this.ImageUrl = dto.ImageUrl;
            this.PrepTime = dto.PreparationTime;
            this.CookTime = dto.CookingTime;
            this.Servings = dto.Servings;
            this.IsNew = false;

            this.Instructions = new ObservableCollection<string>(
    dto.RecipeInstruction.Split('\n').Select(i => i.Trim()));

            this.Ingredients = new ObservableCollection<IngredientEntry>(
                dto.Ingredient.Select(i => new IngredientEntry
                {
                    IngredientName = i,
                    Quantity = 0, // you can't get quantity from plain string
                    Measurement = new MeasurementDTO()
                }));
        }


        private Recipe MapToEntity(ISessionContext session)
        {
            return new Recipe
            {
                Guid = this.Guid, //Guid.NewGuid(),
                RecipeName = this.RecipeName,
                Description = this.Description,
                ImageUrl = this.ImageUrl,
                PreparationTime = this.PrepTime,
                CookingTime = this.CookTime,
                Servings = this.Servings,
                AccountGuid = session.CurrentAccount?.AccountId ?? Guid.Empty,
                Tags = new ObservableCollection<Tag>(Tags.Select(t => new Tag { Guid = Guid.NewGuid(), Name = t })),
                RecipeInstruction = new ObservableCollection<RecipeInstruction>(Instructions.Select((text, idx) => new RecipeInstruction
                {
                    Guid = Guid.NewGuid(),
                    InstructionText = text,
                    Instruction = text,
                    StepNumber = idx + 1,
                    RecipeGuid = this.Guid
                })),
                RecipeToIngredients = Ingredients.Select(ing => new RecipeToIngredient
                {
                    Guid = Guid.NewGuid(),
                    IngredientGuid = Guid.Empty, // To be resolved at service/repository layer
                    RecipeGuid = this.Guid,
                    Quantity = ing.Quantity,
                    Measurement = ing.Measurement.Guid
                }).ToObservableCollection()
            };
        }
    }

    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
            => new ObservableCollection<T>(enumerable);
    }
}



    public class IngredientEntry
    {
        public string IngredientName { get; set; } = string.Empty;
        public MeasurementDTO Measurement { get; set; } = new();
        public double Quantity { get; set; }
    }

