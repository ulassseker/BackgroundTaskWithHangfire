using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackgroundTaskWithHangfire.Models
{
    public class InformationMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("filename")]
        public string Filename { get; set; }
        [BsonElement("height")]
        public int Height { get; set; }
        [BsonElement("width")]
        public int Width { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
        [BsonElement("rating")]
        public int Rating { get; set; }
    }
}