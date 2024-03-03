using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fileservice.Models.Entities
{
    public class Notebook
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NotebookName { get; set; }
        public string BucketName { get; set; }
        public IEnumerable<string> NotebookTags { get; set; }
    }
}
