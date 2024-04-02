using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NotebookService.Models.Entities.ScheduleNotebook;

namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookNode
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NotebookName { get; set; }
        public IEnumerable<NotebookParameter> InputParameters { get; set; }
        public IEnumerable<NotebookParameterToGenerate> InputParameterstoGenerate { get; set; }
        public IEnumerable<string> OutputParametersNames { get; set; }
        public IEnumerable<string> ChildNodeIds { get; set; }
        public string ParentNodeId { get; set; }
        public string StartingNodeId { get; set; }
    }
}
