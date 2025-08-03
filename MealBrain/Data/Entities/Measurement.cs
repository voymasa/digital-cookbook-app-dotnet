using SQLite;
using System;

namespace MealBrain.Data.Entities
{
    [Table("measurement")]
    public class Measurement
    {
        [PrimaryKey, Column("guid"), NotNull]
        public Guid Guid { get; set; }

        [MaxLength(50), Column("measurement_name"), NotNull]
        public string Name { get; set; } = string.Empty;

        [MaxLength(10), Unique, Column("display_name"), NotNull]
        public string DisplayName { get; set; } = string.Empty;

		[Column("wet_or_dry"), NotNull]
		public string Type { get; set; } = string.Empty;

        [Column("num_per_ounce"), NotNull] // this column contains the number per ounce of a measurement which can then be used to convert to other measurments
        public double NumberPerOunce { get; set; }
    }
}
