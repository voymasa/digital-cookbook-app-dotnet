using MealBrain.Data.Entities;

namespace MealBrain.Data.Repositories
{
	public interface IMeasurementRepository
	{
		string GetStatusMessage();
		//Read
		Task<Measurement?> GetMeasurementByGuid(Guid measurementGuid);
		Task<Measurement?> GetMeasurementByName(string name);
		Task<Measurement?> GetMeasurementByAbbr(string abbr);
		Task<List<Measurement>> GetAllMeasurements();
	}
}
