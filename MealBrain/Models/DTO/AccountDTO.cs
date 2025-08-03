namespace MealBrain.Models.DTO
{
    public class AccountDTO
    {
        public Guid? Guid { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public string AccountType { get; set; } = string.Empty;

        // collections; this is where you will add the various collections such as recipes et al
    }
}
