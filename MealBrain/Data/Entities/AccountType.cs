using SQLite;

namespace MealBrain.Data.Entities
{
    [Table("account_type")]
    public class AccountType
    {
        [PrimaryKey, Column("guid"), NotNull]
        public Guid Guid { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
