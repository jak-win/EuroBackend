using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace EuroBackend.Models
{
    public class Prediction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("group")]
        public string Group { get; set; } = null!;

        [BsonElement("matchIndex")]
        public int MatchIndex { get; set; }
        // cos tutaj zmienic [BsonRepresentation(BsonType.Document)]

        [BsonElement("usersPredictions")]
        public List<MatchResult> UsersPredictions { get; set; } = new List<MatchResult>();
    }

    public class MatchResult
    {
        
        public int ScoreA { get; set; }

        
        public int ScoreB { get; set; }
    }
}
