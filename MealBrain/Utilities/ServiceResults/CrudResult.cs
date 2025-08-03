
using MealBrain.Models.DTO;

namespace MealBrain.Utilities.ServiceResults
{
    public class CrudResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ResultMessage { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static CrudResult<T> Ok(T data, string message = "") => new CrudResult<T> 
                                                                        { IsSuccess = true, Data = data, ResultMessage = message };
        

        public static CrudResult<T> Fail(string message) => new CrudResult<T> 
                                                             { IsSuccess = false, Data = default, ResultMessage = message };
    }

}
