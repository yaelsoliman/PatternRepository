namespace PatternRepository.Rsponsess
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Result { get; set; }
        public bool Status { get => Result is null ? false : true; }

    }

    public class ResponseList<T> : Response<DataList<T>>
    { }

    public class DataList<T>
    {
        public IEnumerable<T>? Data { get; set; }
        public int CurrentPage { get; set; }
    }
}
