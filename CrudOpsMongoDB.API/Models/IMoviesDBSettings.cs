namespace CrudOpsMongoDB.API.Models
{
    public interface IMoviesDBSettings
    {
        string? MoviesCollectionName { get; set; }
        string? ConnectionString { get; set; }
        string? DatabaseName { get; set; }
    }
}
