
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace MealBrain.Data.Entities
{

    [Table("recipe")]
    public class Recipe
    {
        // Use Guid for the primary key because they are more secure and unique than integers, and
        // if a record is deleted we will have gaps in our integers but not with guids.
        // Also, Column names should be snake_casing for ease of use with raw sql and using sql directly
        // with the database, if needed (avoids casing issues that can occur with some sql).
        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }

        // put other table columns here
        [MaxLength(100), Column("recipe_name")]
        public string RecipeName { get; set; } = string.Empty;


        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("preparation_time")]
        public int PreparationTime { get; set; } = 0; // in minutes

        [Column("cooking_time")]
        public int CookingTime { get; set; } = 0; // in minutes

        [Column("servings")]
        public int Servings { get; set; } = -1;

        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("account_guid")]
        [ForeignKey(typeof(Account))]
        public Guid AccountGuid { get; set; } = Guid.Empty;


        // Each recipe has a list of zero or more ingredients
        // The OneToMany attribute to the collection of Ingredient will establish the relation at runtime and find the appropriate
        // foreign keys and inverse relationships at that point.
        [ManyToMany(typeof(RecipeToIngredient))]
        public ObservableCollection<Ingredient> Ingredients { get; set; } = [];

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<RecipeToIngredient> RecipeToIngredients { get; set; } = [];

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<RecipeInstruction> RecipeInstruction { get; set; } = [];

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<RecipeToTag> RecipeToTag { get; set; } = [];

        [ManyToMany(typeof(RecipeToTag))]
        public ObservableCollection<Tag> Tags { get; set; } = [];
        //public string Name { get; internal set; }
    }
}

