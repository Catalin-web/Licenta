using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fileservice.Models.Entities
{
    public class NotebookWithParametersAndMetadata
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NotebookName { get; set; }
        public string BucketName { get; set; }
        public IEnumerable<NotebookParameter> NotebookParameters { get; set; }
        public IEnumerable<string> NotebookTags { get; set; }
    }
}
