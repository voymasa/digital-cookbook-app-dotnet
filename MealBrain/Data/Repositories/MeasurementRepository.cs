using MealBrain.Data.Entities;
using MealBrain.Utilities.Helpers;
using SQLite;

namespace MealBrain.Data.Repositories
{
    /// <summary>
    /// Measurement and MeasurementConversion are lookup tables so we shouldn't need to
    /// have any methods except to get the measurement and measurement conversions.
    /// </summary>
    public class MeasurementRepository : BaseRepository, IMeasurementRepository
    {
        public MeasurementRepository(string dbPath) : base(dbPath) { }

		public async Task<List<Measurement>> GetAllMeasurements()
		{
			List<Measurement> measurements = new List<Measurement>();
			try
			{
				await Init();

				measurements = await _connection.Table<Measurement>().ToListAsync();
				StatusMessage = string.Format("Retrieved measurements list.");
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to retrieve measurement data. {0}", ex.Message);
			}

            return measurements;
        }

        public async Task<Measurement?> GetMeasurementByAbbr(string abbr)
        {
            try
            {
                await Init();

                var measurement = await _connection.Table<Measurement>().Where(m => m.DisplayName.Equals(abbr)).FirstAsync();
                StatusMessage = string.Format("Retrieved measurment {0}", measurement.DisplayName);
                return measurement;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve measurement with display name {0}. {1}", abbr, ex.Message);
            }

            return null;
        }

        public async Task<Measurement?> GetMeasurementByName(string name)
        {
            try
            {
                await Init();

                var measruement = await _connection.Table<Measurement>().Where(m => m.Name.Equals(name)).FirstAsync();
                StatusMessage = string.Format("Retrieved measurment {0}", measruement.Name);
                return measruement;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve measurement with display name {0}. {1}", name, ex.Message);
            }

            return null;
        }

        public async Task<Measurement?> GetMeasurementByGuid(Guid measurementGuid)
        {
            try
            {
                await Init();

                var measruement = await _connection.Table<Measurement>().Where(m => m.Guid.Equals(measurementGuid)).FirstAsync();
                StatusMessage = string.Format("Retrieved measurment {0}", measruement.Guid);
                return measruement;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve measurement with display name {0}. {1}", measurementGuid, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// The Init method opens a connection to the database and makes sure that the table is created (if not exists)
        /// </summary>
        protected override async Task Init()
        {
            if (_connection is not null)
            {
                return;
            }

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Measurement>();
            await InitializeTables();
        }

        protected override async Task InitializeTables()
        {
            // create each measurement entity here and then insert them all into the table
            // create each conversion here and then insert them all into the table
            // will need to leverage get measurement by name to streamline this
            // measurements
            List<Measurement> list = new List<Measurement>();
            List<MeasurementTuple> measures = new List<MeasurementTuple>();
            measures.Add(new("cup", "c", 0.125, "Wet"));
            measures.Add(new("gram", "g", 28.34952, "Dry"));
            measures.Add(new("kilogram", "kg", 0.02834952, "Dry"));
            measures.Add(new("pound", "lb", 0.0625, "Dry"));
            measures.Add(new("liter", "l", 0.02957344, "Wet"));
            measures.Add(new("milliliter", "ml", 29.57344, "Wet"));
            measures.Add(new("jigger", "j", 1.5, "Wet"));
            measures.Add(new("fluid ounce", "fl oz", 1.0, "Wet"));
            measures.Add(new("dry ounce", "oz", 1.0, "Dry"));
            measures.Add(new("pint", "pt", 0.0625, "Wet"));
            measures.Add(new("quart", "qt", 0.03125, "Wet"));
            measures.Add(new("gallon", "gal", 0.078125, "Wet"));
            measures.Add(new("teaspoon", "tsp", 6.0, "Wet"));
            measures.Add(new("tablespoon", "Tbsp", 2.0, "Wet"));
            measures.Add(new("pinch", "p", 8.0, "Dry"));
            // get current entreis on the table and check to see if the list is missing any
            var currentMeasures = await _connection.Table<Measurement>().CountAsync();
            if (currentMeasures != measures.Count) // if the tables have a different number of records, then make sure the table has all of the records
            {
                foreach (MeasurementTuple measure in measures)
                {
                    list.Add(new Measurement
                    {
                        Guid = Guid.NewGuid(),
                        Name = measure.Measurement,
                        DisplayName = measure.DisplayName,
                        NumberPerOunce = measure.NumPerOunce,
                        Type = measure.Type,
                    });
                }
                await _connection.InsertAllAsync(list);
            }
            else
            {
                await _connection.DeleteAllAsync<Measurement>();
				foreach (MeasurementTuple measure in measures)
				{
					list.Add(new Measurement
					{
						Guid = Guid.NewGuid(),
						Name = measure.Measurement,
						DisplayName = measure.DisplayName,
						NumberPerOunce = measure.NumPerOunce,
						Type = measure.Type,
					});
				}
				await _connection.InsertAllAsync(list);
			}
        }

        protected override Task MigrateTables()
        {
            throw new NotImplementedException();
        }
    }
}
