namespace vehicle_calculation.common.Action
{
    public class Result
    {
        public bool IsError { get; set; } = false;
        public string Error { get; set; } = string.Empty;

        public void SetError(string error)
        {
            IsError = true;
            Error = error;
        }

        public void SetFrom(Result from)
        {
            IsError = from.IsError;
            Error = from.Error;
        }
    }
}
