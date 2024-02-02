using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend165.DTOs
{
    public class OrderPostDto
    {
        public string kundenname { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string Priorität { get; set; }

        public string Dienstleistung { get; set; }

        public string Status { get; set; }
    }
}
