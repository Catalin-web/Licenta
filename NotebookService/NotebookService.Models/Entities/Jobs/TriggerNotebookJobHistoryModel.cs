using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotebookService.Models.Entities.Jobs
{
    public class TriggerNotebookJobHistoryModel
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TriggerNotebookJobId { get; set; }
        public string ScheduledNotebookId { get; set; }
        public DateTime TriggerTime { get; set; }
    }
}
