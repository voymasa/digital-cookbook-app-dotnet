using MealBrain.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MealBrain.Utilities.Services;

namespace MealBrain.Models
{
    public class RecipeCardModel
    {
        public Guid Guid { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Servings { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public string CookTime { get; set; } = string.Empty;

                
    }
}
