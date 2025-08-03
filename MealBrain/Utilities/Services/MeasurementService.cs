using MealBrain.Data.Repositories;
using MealBrain.Models.DTO;
using MealBrain.Utilities.Helpers;
using MealBrain.Utilities.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.Utilities.Services
{
	public class MeasurementService : IMeasurementService
	{
		private readonly IMeasurementRepository _measurementRepository;

		public MeasurementService(IMeasurementRepository measurementRepository)
		{
			_measurementRepository = measurementRepository;
		}

		/// <summary>
		/// This method handles the request to get all of the records in the measurement lookup table
		/// and return a list of dtos to the caller.
		/// </summary>
		/// <returns>A crud result containing the list of dtos or an error to be handled by the caller</returns>
		public async Task<CrudResult<List<MeasurementDTO>>> GetMeasurementsList()
		{
			var requestResult = new CrudResult<List<MeasurementDTO>>();
			var repoResult = await _measurementRepository.GetAllMeasurements();

			if (repoResult.Any()) // consider it a success if any results are returned
			{
				requestResult.IsSuccess = true;
			}
			requestResult.ResultMessage = _measurementRepository.GetStatusMessage();
			requestResult.Data = MeasurementHelper.MapEntitiesToDtos(repoResult);

			return requestResult;
		}
	}
}
