using MongoDB.Bson.Serialization.Attributes;

namespace POC.WebAPI.Models
{
    public class Account : BaseModel
    {
        [BsonRequired]
        public string Username { get; set; }

        [BsonRequired]
        public string Password { get; set; }
    }
}
