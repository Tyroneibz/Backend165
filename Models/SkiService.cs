using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend165.Models
{
    public class SkiService
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("kundenname")]
        public string kundenname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("telefon")]
        public string Telefon { get; set; }

        [BsonElement("priorität")]
        public string Priorität { get; set; }

        [BsonElement("dienstleistung")]
        public string Dienstleistung { get; set; }
        // Stellen Sie sicher, dass Sie diese Zeile hinzufügen:

        [BsonElement("status")]
        public string Status { get; set; }
    }
}
