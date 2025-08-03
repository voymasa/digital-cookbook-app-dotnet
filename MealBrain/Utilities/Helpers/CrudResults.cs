//using System;
//using MealBrain.Utilities;

//namespace MealBrain.Utilities
//{
//    public class CrudResult<T>
//    {
//        public bool Success { get; set; }
//        public string Message { get; set; } = string.Empty;
//        public T Data { get; set; }

//        public static CrudResult<T> Ok(T data, string message = "") =>
//            new CrudResult<T> { Success = true, Data = data, Message = message };

//        public static CrudResult<T> Fail(string message) =>
//            new CrudResult<T> { Success = false, Data = default, Message = message };

//        public bool IsSuccess => Success;
//        public string ResultMessage => Message;
//    }
//}
