namespace CrudOpsMongoDB.API.Models
{
    public class ResponseMessage
    {
        public bool IsSuccess {  get; set; }
        public int? StatusCode { get; set; }
        public List<MoviesColl>? Data { get; set; }
        public string? Message { get; set; }
    }
}
