using CrudOpsMongoDB.API.Models;
using CrudOpsMongoDB.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CrudOpsMongoDB.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MovieCollController : ControllerBase
    {
        private readonly IMovieService _movies;
        public MovieCollController(IMovieService movies)
        {
                _movies = movies;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.GetAllMoviesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieDetailsById(string id)
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.GetMovieByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieDetailsByParticularName(string name)
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.GetMovieByNameAsync(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieDetailsBySimilarName(string name)
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.GetMovieBySimilarNameAsync(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertSingleMovie(MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.CreateMovieAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertMultipleMovies(List<MoviesColl> model)
        {
            ResponseMessage response = new();
            try
            {
                response = await _movies.InsertMultipleMoviesAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSingleMovie(string id, MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                var details = await _movies.UpdateMovieAsync(id, model);
                return Ok(details);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateMovieBudgetById(string id, MoviesColl model)
        {
            ResponseMessage response = new();
            try
            {
                var details = await _movies.UpdateMovieBudgetByIdAsync(id, model);
                return Ok(details);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMovieById(string id)
        {
            ResponseMessage response = new();
            try
            {
                var details = await _movies.DeleteMovieAsync(id);
                return Ok(details);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }

    }
}
