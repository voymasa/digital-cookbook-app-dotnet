using SQLite;
using MealBrain.Data.Entities;

namespace MealBrain.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly string _dbPath;
        protected SQLiteAsyncConnection? _connection;
        protected string StatusMessage { get; set; } = string.Empty;

        public string GetStatusMessage() { return StatusMessage; }

        public BaseRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        /// <summary>
        /// Override this method with the specific connection implementation and table(s).
        /// You should also call the InitializeTables method after the table declarations.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        protected abstract Task Init();

        /// <summary>
        /// Override this method with a check to see if the table has any information, and if
        /// not then to populate it with the default information (i.e. lookup table information).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        protected abstract Task InitializeTables();

        /// <summary>
        /// Override this method with checks for each record that needs to be in the table is, and
        /// if not then populate the information
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        protected abstract Task MigrateTables();
    }
}
