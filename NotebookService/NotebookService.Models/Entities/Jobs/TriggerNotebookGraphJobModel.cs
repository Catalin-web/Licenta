using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotebookService.Models.Entities.Jobs
{
    public class TriggerNotebookGraphJobModel
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string JobName { get; set; }
        public string NotebookNodeId { get; set; }
        public string JobId { get; set; }
        public string TriggerId { get; set; }
        public int TriggerJobInterval { get; set; }
        public string UserId { get; set; }
    }
}
