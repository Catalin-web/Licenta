using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotebookService.Models.Entities.Jobs
{
    public class TriggerNotebookGraphJobHistoryModel
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TriggerNotebookGraphJobId { get; set; }
        public string GraphUniqueId { get; set; }
        public DateTime TriggerTime { get; set; }
    }
}
