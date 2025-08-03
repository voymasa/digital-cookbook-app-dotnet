using MealBrain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.Models.DTO
{
    public class RecipeDTO
    {
        public Guid? Guid { get; set; }
        public required string RecipeName { get; set; }
        public required List<string> Ingredient { get; set; }
        public required string RecipeInstruction { get; set; }
        public required string Description { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Servings { get; set; }
        public required string ImageUrl { get; set; }
    }

	/*public class RecipeDetailsDTO
	{ 
		// Create RecipeDetailsDTO class
		// flatten lists to basic strings to transfer between the Entity, DTO, UI 
		public List<string> Ingredients { get; set; } = new();
		public List<string> Tags { get; set; } = new();
		public List<string> Instructions { get; set; } = new();
    }
	*/
}
