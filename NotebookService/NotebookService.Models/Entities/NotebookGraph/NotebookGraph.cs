using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotebookService.Models.Entities.NotebookGraph
{
    public class NotebookGraph
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public NotebookNode RootNode { get; set; }
    }
}
