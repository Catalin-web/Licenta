using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GeneratorService.Models.Entities.Jobs
{
    public class JobBase
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public JobType JobType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Progress Progress { get; set; }
        public Status Status { get; set; }
    }
}
