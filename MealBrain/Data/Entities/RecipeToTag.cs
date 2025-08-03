

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.Data.Entities
{

    [Table("recipe_to_tag")]
    public class RecipeToTag
    {

        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }

        [ForeignKey(typeof(Recipe))]
        [Column("recipe_guid")]
        public Guid RecipeGuid { get; set; }

        [ForeignKey(typeof(Tag))]
        [Column("tag_guid")]

        public Guid TagGuid { get; set; }
    }
}

