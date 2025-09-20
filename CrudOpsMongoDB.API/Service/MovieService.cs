using CrudOpsMongoDB.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;

namespace CrudOpsMongoDB.API.Service
{
    public class MovieService : IMovieService
    {
        private IMongoCollection<MoviesColl> _movieColl;

        public MovieService(IMoviesDBSettings settings, IMongoClient client)
        {
            var database =  client.GetDatabase(settings.DatabaseName);
            _movieColl =  database.GetCollection<MoviesColl>(settings.MoviesCollectionName);
        }
		
		//To fetch all the movie details
        public async Task<ResponseMessage> GetAllMoviesAsync()
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                //response.StatusCode = (int)HttpStatusCode.OK;
                //response.Status = (string)HttpStatusCode.OK;
                response.Message = "Records fetched successfully.";
                response.Data = new List<MoviesColl>();
                response.Data = await _movieColl.Find(x => true).ToListAsync();
                if(response.Data.Count == 0)
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                else
                    response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess= false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: "+ ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> GetMovieByIdAsync(string id)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.Data = new List<MoviesColl>();
                response.Data = await _movieColl.Find(x => x.Id == id).ToListAsync();
                if (response.Data.Count == 0)
                {
                    response.Message = "Records with particular Id does not exists.";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = "Records fetched successfully.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> GetMovieByNameAsync(string name)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.Data = new List<MoviesColl>();
                response.Data = await _movieColl.Find(x => x.MovieName == name).ToListAsync();
                if (response.Data.Count == 0)
                {
                    response.Message = "Records with particular name does not exists.";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = "Records fetched successfully.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> GetMovieBySimilarNameAsync(string name)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.Data = new List<MoviesColl>();
                //var filter = Builders<BsonDocument>.Filter.Regex("movieName", new BsonRegularExpression("Coolie", "i")).ToString();
                var filter = Builders<MoviesColl>.Filter.Regex(x => x.MovieName, new BsonRegularExpression(name, "i"));     //To fetch the movies containing similar maes. Similar to LIKE operator
                response.Data = await _movieColl.Find(filter).ToListAsync();
                if (response.Data.Count == 0)
                {
                    response.Message = "Records with name does not exists.";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = "Records fetched successfully.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> CreateMovieAsync(MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                await _movieColl.InsertOneAsync(model);
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Records have been inserted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> InsertMultipleMoviesAsync(List<MoviesColl> model)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                await _movieColl.InsertManyAsync(model);
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Records have been inserted successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> UpdateMovieAsync(string id, MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                ResponseMessage response1 = await GetMovieByIdAsync(id);
                if(response1 != null)
                {
                    var result = await _movieColl.ReplaceOneAsync(x => x.Id == id, model);
                    if(!result.IsAcknowledged)          // "result.IsAcknowledged" remains true if any update occurs in the table.
                    {
                        response.IsSuccess = false;
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        response.Message = "Records not updated.";
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Record updated successfully.";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode= (int)HttpStatusCode.NotFound;
                    response.Message = "Record with given Id does not exists";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode= (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: "+ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> UpdateMovieBudgetByIdAsync(string id, MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                ResponseMessage response1 = await GetMovieByIdAsync(id);
                if (response1 != null)
                {
                    var result = await _movieColl.ReplaceOneAsync(x => x.Id == id, model);
                    if (!result.IsAcknowledged)          // "result.IsAcknowledged" remains true if any update occurs in the table.
                    {
                        response.IsSuccess = false;
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        response.Message = "Records not updated.";
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Record updated successfully.";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Record with given Id does not exists";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
        public async Task<ResponseMessage> DeleteMovieAsync(string id)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                var details = await GetMovieByIdAsync(id);
                if(details != null)
                {
                    var result = await _movieColl.DeleteOneAsync(x => x.Id == id);
                    if (!result.IsAcknowledged)          // "result.IsAcknowledged" remains true if any update occurs in the table.
                    {
                        response.IsSuccess = false;
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        response.Message = "Records not deleted.";
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Record deleted successfully.";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Record with given Id does not exists";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return response;
            }
        }
    }
}
