using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MealBrain.Data.Entities
{
    /// <summary>
    /// This class represents the "intermediate" relationship between two "many to many" entities.
    /// Unlike other ORMs and raw sql, it does not appear that sqlite-net extensions requires an actual
    /// entity/table for the joiner table here.
    /// </summary>
    public class RecipeToIngredient
    {
        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }

        [ForeignKey(typeof(Recipe))]

        [Column("recipe_guid")]

        public Guid RecipeGuid { get; set; }

        [Column("ingredient_guid")]
        public Guid IngredientGuid { get; set; }

        [Column("quantity")]
        public double Quantity { get; set; }

        [MaxLength(50), Column("measurement_guid")]
        [ForeignKey(typeof(Measurement))]
        public Guid Measurement { get; set; }

        [ManyToOne(nameof(RecipeGuid))]
        public Recipe Recipe { get; set; }

        [ManyToOne(nameof(IngredientGuid))]
        public Ingredient Ingredient { get; set; }

    }
}
