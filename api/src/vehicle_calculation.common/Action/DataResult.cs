using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vehicle_calculation.common.Action
{
    public class DataResult<T> : Result
    {
        public T? Data { get; set; }

        public static DataResult<T> Success(T? data = default)
        {
            return new DataResult<T>
            {
                Data = data
            };
        }

        public static DataResult<T> Failure(string error)
        {
            var response = new DataResult<T>();
            response.SetError(error);
            return response;
        }
    }
}
