namespace CrudOpsMongoDB.API.Models
{
    public class MoviesDBSettings : IMoviesDBSettings
    {
        public string? MoviesCollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
