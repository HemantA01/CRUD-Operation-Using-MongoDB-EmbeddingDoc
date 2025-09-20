using CrudOpsMongoDB.API.Models;

namespace CrudOpsMongoDB.API.Service
{
    public interface IMovieService
    {
        Task<ResponseMessage> GetAllMoviesAsync();
        Task<ResponseMessage> GetMovieByIdAsync(string id);
        Task<ResponseMessage> GetMovieByNameAsync(string name);
        Task<ResponseMessage> GetMovieBySimilarNameAsync(string name);
        Task<ResponseMessage> CreateMovieAsync(MoviesColl model);
        Task<ResponseMessage> InsertMultipleMoviesAsync(List<MoviesColl> model);
        Task<ResponseMessage> UpdateMovieAsync(string id,MoviesColl model);
        Task<ResponseMessage> UpdateMovieBudgetByIdAsync(string id,MoviesColl model);
        Task<ResponseMessage> DeleteMovieAsync(string id);
    }
}
