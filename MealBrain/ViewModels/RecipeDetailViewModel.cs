using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MealBrain.Data.Entities;
using MealBrain.Data.Repositories;
using MealBrain.Models;
using MealBrain.Models.DTO;
using MealBrain.Utilities.Services;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;

namespace MealBrain.ViewModels
{
    [Preserve(AllMembers = true)]
    public partial class RecipeDetailViewModel : BaseViewModel
    {
        private readonly ISessionContext _session;

        // Expose the model as 'Recipes' 
        public RecipeDetailModel Recipes { get; }

        public ObservableCollection<MeasurementDTO> Measurements { get { return Recipes.Measurements; } }
        public ObservableCollection<string> Instructions { get { return Recipes.Instructions;  } }
        public ObservableCollection<IngredientEntry> Ingredients { get { return Recipes.Ingredients; } }

        // Properties for recipe details
        public string RecipeName
        {
            get => Recipes.RecipeName;
            set
            {
                if (Recipes.RecipeName != value)
                {
                    Recipes.RecipeName = value;
                    RaisePropertyChanged(nameof(RecipeName));
                }
            }
        }

        public string Description
        {
            get => Recipes.Description;
            set
            {
                if (Recipes.Description != value)
                {
                    Recipes.Description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public int PrepTime
        {
            get => Recipes.PrepTime;
            set
            {
                if (Recipes.PrepTime != value)
                {
                    Recipes.PrepTime = value;
                    RaisePropertyChanged(nameof(PrepTime));
                }
            }
        }

        public int CookTime
        {
            get => Recipes.CookTime;
            set
            {
                if (Recipes.CookTime != value)
                {
                    Recipes.CookTime = value;
                    RaisePropertyChanged(nameof(CookTime));
                }
            }
        }


        public int Servings
        {
            get => Recipes.Servings;
            set
            {
                if (Recipes.Servings != value)
                {
                    Recipes.Servings = value;
                    RaisePropertyChanged(nameof(Servings));
                }
            }
        }

        public string ImageUrl
        {
            get => Recipes.ImageUrl;
            set
            {
                if (Recipes.ImageUrl != value)
                {
                    Recipes.ImageUrl = value;
                    RaisePropertyChanged(nameof(ImageUrl));
                }
            }
        }

        public RecipeDetailViewModel(RecipeDetailModel recipes, ISessionContext session)
        {
            Recipes = recipes;
            _session = session;
            PickImageCommand = new AsyncRelayCommand(PickImageAsync);
        }

        private string _ingredientNameInput = string.Empty;
        public string IngredientNameInput
        {
            get => _ingredientNameInput;
            set
            {
                if (_ingredientNameInput != value)
                {
                    _ingredientNameInput = value;
                    //RaisePropertyChanged();
                }
            }
        }

        public string QuantityInput
        {
            get
            {
                return Recipes.NewIngredient.Quantity.ToString();
            }
            set
            {
                if (!Recipes.NewIngredient.Quantity.ToString().Equals(value))
                {
                    double quantity;
                    if (double.TryParse(value, out quantity))
                    {
                        Recipes.NewIngredient.Quantity = quantity;
                        RaisePropertyChanged(nameof(QuantityInput));
                    }
                }
            }
        }

        /*public MeasurementDTO MeasurementInput
        {
            get {
                return Recipes.NewIngredient.Measurement;
            }
            set {
                if (!Recipes.NewIngredient.Measurement.Guid.Equals(((MeasurementDTO)value).Guid))
                {
                    Recipes.NewIngredient.Measurement = value;
                    RaisePropertyChanged(nameof(MeasurementInput));
                }
            }
        } */

        public string NewInstruction
        {
            get { 
                return Recipes.NewInstruction; 
            } 
            set { 
                if (!Recipes.NewInstruction.Equals(value)) { 
                    Recipes.NewInstruction = value;
                    RaisePropertyChanged(nameof(NewInstruction));
                } 
            }
        }

        // Property for Tags binding as comma separated string
        public string TagsText
        {
            get => string.Join(", ", Recipes.Tags);
            set
            {
                var tags = value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Distinct()
                                .ToList() ?? new System.Collections.Generic.List<string>();

                if (!tags.SequenceEqual(Recipes.Tags))
                {
                    Recipes.Tags = tags;
                    RaisePropertyChanged(nameof(TagsText));
                }
            }
        }

        public ICommand AddInstructionCommand => new AsyncRelayCommand(AddInstruction);
        public ICommand AddIngredientCommand => new AsyncRelayCommand(AddIngredient);
        public ICommand SaveCommand => new Command(async () => await SaveAsync());
        public ICommand DeleteCommand => new Command(async () => await DeleteAsync());
        public ICommand PickImageCommand { get; }


        public async void LoadMeasurements()
        {
            await Recipes.LoadMeasurementList();
            RaisePropertyChanged(nameof(Measurements));
        }

        public async Task AddInstruction()
        {
            if (!string.IsNullOrWhiteSpace(NewInstruction))
            {
                await Recipes.AddInstruction();
                RaisePropertyChanged(nameof(Instructions));
                RaisePropertyChanged(nameof(NewInstruction));
            }
        }

        public async Task AddIngredient()
        {
            if (!string.IsNullOrWhiteSpace(IngredientNameInput) &&
                double.TryParse(QuantityInput, out double qty))//&&
                //MeasurementInput != null)
            {
                await Recipes.AddIngredient();
                RaisePropertyChanged(nameof(IngredientNameInput));
                RaisePropertyChanged(nameof(QuantityInput));
                //RaisePropertyChanged(nameof(MeasurementInput));
                RaisePropertyChanged(nameof(Ingredients));
            }
        }

        public async Task LoadAsync(Guid recipeId)
        {
            await Recipes.LoadAsync(recipeId);

            // Notify UI of changes to bound properties (in case the model was replaced or updated deeply)
            RaisePropertyChanged(nameof(Recipes));
            RaisePropertyChanged(nameof(TagsText));
            RaisePropertyChanged(nameof(RecipeName));
            RaisePropertyChanged(nameof(Description));
            RaisePropertyChanged(nameof(PrepTime));
            RaisePropertyChanged(nameof(CookTime));
            RaisePropertyChanged(nameof(Servings));
            RaisePropertyChanged(nameof(ImageUrl));
        }

        public async Task LoadRecipeAsync(Guid recipeGuid)
        {
            await Recipes.LoadAsync(recipeGuid);
        }

        private async Task SaveAsync()
        {
            await Recipes.SaveAsync(_session);
        }

        private async Task DeleteAsync()
        {
            await Recipes.DeleteAsync();
        }

        private async Task PickImageAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                var fileName = result.FileName;

         
                string Image = result.FullPath;

                
                Recipes.ImageUrl = Image;
                RaisePropertyChanged(nameof(ImageUrl));
            }
        }
    }
} 