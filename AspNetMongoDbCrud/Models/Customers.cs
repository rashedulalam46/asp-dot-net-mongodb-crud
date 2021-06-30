using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AspNetMongoDbCrud.Models
{
    public class Customers
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public int CustomerId { get; set; }

        [BsonElement]
        public string FirstName { get; set; }

        [BsonElement]
        public string LastName { get; set; }

        [BsonElement]
        public string Phone { get; set; }

        [BsonElement]
        public string Email { get; set; }

        [BsonElement]
        public decimal Point { get; set; }
    }
}
