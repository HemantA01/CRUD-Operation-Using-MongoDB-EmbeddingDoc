using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

namespace CrudOpsMongoDB.API.Models
{
    public class MovieReviews
    {
        [BsonElement("rating")]
        public decimal? Rating {  get; set; }
        [BsonElement("reviewerName")]
        public string? ReviewerName {  get; set; }
        [BsonElement("reviewerText")]
        public string? ReviewerText {  get; set; }
    }
}
