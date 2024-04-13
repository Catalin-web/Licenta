using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookScheduledNode
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public IEnumerable<string> ChildScheduledNodeIds { get; set; }
    }
}
