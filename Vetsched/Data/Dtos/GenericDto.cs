namespace Vetsched.Data.Dtos
{
    public class GenericDto<T>
    {
        public string Token { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }
    }
}
