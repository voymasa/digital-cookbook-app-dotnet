using CommunityToolkit.Maui.Core.Extensions;
using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;
using MealBrain.Utilities.Services;
using System.Collections.ObjectModel;

namespace MealBrain.Models
{
	public class MeasurementModel
	{
		private IMeasurementService _service;

		public MeasurementModel(IMeasurementService service) 
		{
			_service = service;
		}

		public string InitialAmount { get; set; } = "0";
		public string ConvertedAmount { get; set; } = "0";

		public ObservableCollection<MeasurementDTO> Measurements { get; set; } = [];

		public MeasurementDTO? MeasurementFrom { get; set; }

		public MeasurementDTO? MeasurementTo { get; set; }

		// for notifications and feedback to user
		public bool WasSuccess { get; set; }
		public string NotificationMessage { get; set; } = string.Empty;

		public async Task LoadMeasurementList()
		{
			CrudResult<List<MeasurementDTO>> result = await _service.GetMeasurementsList();
			NotificationMessage = result.ResultMessage;
			WasSuccess = result.IsSuccess;

			if (WasSuccess)
			{
				Measurements = result.Data?.ToObservableCollection<MeasurementDTO>();
			}
		}

		public async Task CalculateAmount()
		{
			// calculate the conversion between the two amounts and return it
			// formula is initial amount * measurement from factor / measurement to factor;
			NotificationMessage = string.Empty; // empty the string so that the viewmodel can check if there is a message, and if so know there is an error to raise property notification change for the property
			ConvertedAmount = string.Empty; // empty the previous value displayed; if it hits an error state then it will make the previous converted value disappear anyway
			if (MeasurementFrom == null || MeasurementTo == null) 
			{
				NotificationMessage = string.Format("Must select which measurements to convert between");
				return;
			};
			if (!MeasurementFrom.Type.Equals(MeasurementTo.Type))
			{
				NotificationMessage = string.Format("Both Measurements must be same type (wet or dry)");
				return;
			}
			double from = MeasurementFrom!.PerOunce;
			double to = MeasurementTo!.PerOunce;
			double initial; 
			if(!double.TryParse(InitialAmount, out initial))
			{
				NotificationMessage = string.Format("Initial Amount must be a number");
				return;
			}

			ConvertedAmount = string.Format("{0:0.####}", (initial / from * to)); // convert initial amount to ounces then to the new measure
		}
	}
}
