
﻿

﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.Data.Entities
{

    [Table("tag")]
    public class Tag
    {
        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }

        [MaxLength(60), Column("name")]
        public string Name { get; set; } = string.Empty;

        [ManyToMany(typeof(RecipeToTag))]
        public ObservableCollection<Recipe> Recipes { get; set; } = new ObservableCollection<Recipe>();
    }
}

