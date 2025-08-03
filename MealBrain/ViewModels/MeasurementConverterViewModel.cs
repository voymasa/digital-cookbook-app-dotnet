using CommunityToolkit.Mvvm.Input;
using MealBrain.Models;
using MealBrain.Models.DTO;
using System.Collections.ObjectModel;

namespace MealBrain.ViewModels
{
	public class MeasurementConverterViewModel : BaseViewModel
	{
		private MeasurementModel _model;
		public IAsyncRelayCommand CalculateCommand { get; }
		public MeasurementConverterViewModel(MeasurementModel model) 
		{
			_model = model;
			CalculateCommand = new AsyncRelayCommand(CalculateConvertedAmount);
		}

		public string Notification
		{
			get
			{
				return _model.NotificationMessage;
			}
		}

		public bool Success
		{
			get
			{
				return _model.WasSuccess;
			}
		}

		public string InitialAmount {
			get
			{
				return _model.InitialAmount;
			} 
			set
			{
				if(!_model.InitialAmount.Equals(value))
				{
					_model.InitialAmount = value;
					RaisePropertyChanged(nameof(InitialAmount));
				}
			}
		}

		public ObservableCollection<MeasurementDTO> Measurements
		{
			get
			{
				return _model.Measurements;
			}
		}

		public MeasurementDTO MeasurementFrom
		{
			set
			{
				if(_model.MeasurementFrom == null || _model.MeasurementFrom.Guid != value.Guid)
				{
					_model.MeasurementFrom = value;
					RaisePropertyChanged(nameof(MeasurementFrom));
				}
			}
		}

		public MeasurementDTO MeasurementTo
		{
			set
			{
				if(_model.MeasurementTo == null || _model.MeasurementTo.Guid != value.Guid)
				{
					_model.MeasurementTo = value;
					RaisePropertyChanged(nameof(MeasurementTo));
				}
			}
		}

		public string ConvertedAmount
		{
			get
			{
				return _model.ConvertedAmount;
			}
			set
			{
				if (!value.Equals(_model.ConvertedAmount))
				{
					_model.ConvertedAmount = value;
					RaisePropertyChanged(nameof(ConvertedAmount));
				}
			}
		}

		public async void LoadMeasurements()
		{
			await _model.LoadMeasurementList();
			RaisePropertyChanged(nameof(Measurements));
		}

		private async Task CalculateConvertedAmount()
		{
			await _model.CalculateAmount();
			RaisePropertyChanged(nameof(Notification));
			RaisePropertyChanged(nameof(ConvertedAmount));
		}
	}
}
