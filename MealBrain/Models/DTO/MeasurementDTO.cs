namespace MealBrain.Models.DTO
{
	public class MeasurementDTO
	{
		public Guid Guid { get; set; }
		public string Name { get; set; } = string.Empty;
		public string DisplayName { get; set; } = string.Empty;
		public string Type { get; set; } = string.Empty;
		public double PerOunce { get; set; }
	}
}
