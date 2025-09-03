namespace BootCamp1_Api.ViewModels
{
    public class ApiResponse<T>
    {
        public string Msg { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}
