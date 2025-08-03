using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace MealBrain.Data.Entities
{
    [Table("ingredient")]
    public class Ingredient
    {
        // Use Guid for the primary key because they are more secure and unique than integers, and
        // if a record is deleted we will have gaps in our integers but not with guids.
        // Also, Column names should be snake_casing for ease of use with raw sql and using sql directly
        // with the database, if needed (avoids casing issues that can occur with some sql).
        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }


        // put other table columns here
        [MaxLength(100), Column("name")]
        public string RecipeName { get; set; } = string.Empty;

        // Many ingredients may appear on a recipe, or may exist with no related recipe, or be on many different recipes
        // The ManyToOne attribute lets it find the foreign keys and inverse relationships to 
        [ManyToMany(typeof(RecipeToIngredient))]
        public ObservableCollection<Recipe> Recipes { get; set; } = [];

		// put other table columns here
		[MaxLength(100), Column("ingredient_name")]
        public string IngredientName { get; set; } = string.Empty;

		//public List<Recipe> Recipes { get; set; } = new List<Recipe>();

		// Many ingredients may appear on a recipe, or may exist with no related recipe, or be on many different recipes
		// The ManyToOne attribute lets it find the foreign keys and inverse relationships to 
		[OneToMany]
        public ObservableCollection<RecipeToIngredient> RecipeIngredients { get; set; } = [];

    }
}
