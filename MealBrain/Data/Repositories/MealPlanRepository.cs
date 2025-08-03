namespace MealBrain.Data.Repositories
{
    public class MealPlanRepository : BaseRepository, IMealPlanRepository
    {
        public MealPlanRepository(string dbPath) : base(dbPath)
        {
        }

        protected override async Task Init()
        {
            throw new NotImplementedException();
        }

        protected override async Task InitializeTables()
        {
            throw new NotImplementedException();
        }

        protected override Task MigrateTables()
        {
            throw new NotImplementedException();
        }
    }
}
