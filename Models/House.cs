using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    [DataContract]
    public class House
    {
        [DataMember] [BsonId] public ObjectId Id { get; set; }
        [DataMember] [BsonElement("MDB_Title")] public string Title { get; set; }
        [DataMember] [BsonElement("MDB_UrlPhoto")] public string UrlPhoto { get; set; }
        [DataMember] [BsonElement("MDB_City")] public string City { get; set; }
        [DataMember] [BsonElement("MDB_Price")] public int Price { get; set; }
        [DataMember] [BsonElement("MDB_SQM")] public int SQM { get; set; }
        [DataMember] [BsonElement("MDB_Rooms")] public int Rooms { get; set; }
        [DataMember] [BsonElement("MDB_Bathrooms")] public int Bathrooms { get; set; }
    }
}
