using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudOpsMongoDB.API.Models
{
    [BsonIgnoreExtraElements]       //Used to ignore if we have extra elements in MongoDN not to be used in .Net
    public class MoviesColl
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("movieName")]
        public string? MovieName { get; set; }
        [BsonElement("releaseDate")]
        public string? ReleaseDate { get; set; }
        [BsonElement("releaseYear")]
        public int? ReleaseYear { get; set; }
        public string[]? Actors { get; set; }
        [BsonElement("genere")]
        public string[]? Genere { get; set; }
        [BsonElement("reviews")]
        public List<MovieReviews>? Reviews { get; set; }
        [BsonElement("directorName")]
        public string[]? DirectorName { get; set; }
        [BsonElement("movieSummary")]
        public string? MovieSummary { get; set; }
        [BsonElement("budget")]
        public double? Budget { get; set; }
        [BsonElement("revenueCollection")]
        public double? RevenueCollection { get; set; }
        [BsonElement("languagesReleased")]
        public string[]? LanguagesReleased { get; set; }
    }
}
