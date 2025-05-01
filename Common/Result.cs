namespace SalonBooking.Common
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new();
        public T Value { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T> { Succeeded = true, Value = value };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>
            {
                Succeeded = false,
                Errors = new List<string> { error }
            };
        }
    }
}
