using MealBrain.Data.Entities;
using MealBrain.Models;
using MealBrain.Models.DTO;

namespace MealBrain.Utilities.Helpers
{
    public static class MeasurementHelper
    {
        public static MeasurementDTO MapEntityToDto(Measurement from)
        {
            return new MeasurementDTO{
                Guid = from.Guid,
                Name = from.Name,
                DisplayName = from.DisplayName,
                PerOunce = from.NumberPerOunce,
                Type = from.Type,
            };
        }

        public static Measurement MapDtoToEntity(MeasurementDTO from)
        {
            return new Measurement {
                Guid = from.Guid,
                Name = from.Name,
                DisplayName = from.DisplayName,
                NumberPerOunce = from.PerOunce,
                Type = from.Type,
            };
		}

        public static List<MeasurementDTO> MapEntitiesToDtos(List<Measurement> entities)
        {
            List<MeasurementDTO> dtos = new List<MeasurementDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(MapEntityToDto(entity));
            }

            return dtos;
        }
    }

    /// <summary>
    /// The tuple's purpose is for ease of generating the lookup table information
    /// Whether an ingredient is wet or dry is only necessary for getting the "right" conversion because the numbers are predicated on dry or fluid ounces, which are different amounts
    /// </summary>
    public struct MeasurementTuple
    {
        public string Measurement { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public double NumPerOunce { get; set; }
        public MeasurementTuple(string measurementFrom, string displayName, double num, string type)
        {
            this.Measurement = measurementFrom;
            this.DisplayName = displayName;
            this.NumPerOunce = num;
            this.Type = type; // this is for whether wet or dry ingredient
        }
    }

}
