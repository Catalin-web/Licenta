using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Entities.Jobs
{
    public class TriggerNotebookJobModel
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string JobName { get; set; }
        public string NotebookName { get; set; }
        public IEnumerable<NotebookParameter> InputParameters { get; set; }
        public IEnumerable<NotebookParameterToGenerate> InputParameterstoGenerate { get; set; }
        public IEnumerable<string> OutputParametersNames { get; set; }
        public string JobId { get; set; }
        public string TriggerId { get; set; }
        public int TriggerJobInterval { get; set; }
        public string UserId { get; set; }
    }
}
