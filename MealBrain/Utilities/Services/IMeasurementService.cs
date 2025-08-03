using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;

namespace MealBrain.Utilities.Services
{
	public interface IMeasurementService
	{
		Task<CrudResult<List<MeasurementDTO>>> GetMeasurementsList();
	}
}
