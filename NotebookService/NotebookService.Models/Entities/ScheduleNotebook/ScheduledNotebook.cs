using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NotebookService.Models.Entities.NotebookGraph;

namespace NotebookService.Models.Entities.ScheduleNotebook
{
    public class ScheduledNotebook
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NotebookName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Progress Progress { get; set; }
        public Status Status { get; set; }
        public IEnumerable<NotebookParameter> InputParameters { get; set; }
        public IEnumerable<NotebookParameterToGenerate> InputParametersToGenerate { get; set; }
        public IEnumerable<NotebookParameter> OutputParameters { get; set; }
        public IEnumerable<string> OutputParametersNames { get; set; }
        public IEnumerable<NotebookNode> ChildNodes { get; set; }
    }
}
