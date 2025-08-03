

﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.Data.Entities
{


    [Table("recipe_instructions")]
    public class RecipeInstruction
    {
        [PrimaryKey, Column("guid")]
        public Guid Guid { get; set; }

        [ForeignKey(typeof(Recipe))]
        public Recipe Recipe { get; set; }

        [ForeignKey(typeof(Recipe))]
        [Column("recipe_guid")]
        public Guid RecipeGuid { get; set; }


        [Column("step_number")]
        public int StepNumber { get; set; }


        [Column("instruction_text")]
        public string InstructionText { get; set; } = string.Empty;

        [Column("instruction")]
        public string Instruction { get; set; } = string.Empty;

    }
}



