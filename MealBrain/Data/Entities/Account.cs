using MealBrain.Utilities.Services;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MealBrain.Data.Entities
{
    [Table("user_account")]
    public class Account
    {
        [PrimaryKey, Column("guid"), NotNull]
        public Guid Guid { get; set; }

        [MaxLength(100), Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100), Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100), Column("display_name"), NotNull]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(100), Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100), Column("image_path")]
        public string ImagePath { get; set; } = string.Empty;

        [Column("created"), NotNull]
        public DateTime Created { get; set; }
        [Column("modified")]
        public DateTime? Modified { get; set; }

        [ForeignKey(typeof(AccountType)), Column("account_type_guid"), NotNull]
        public Guid AccountTypeGuid { get; set; }

    }
}
