using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    [DataContract]
    public class Animal
    {
        [DataMember][BsonId] public ObjectId Id { get; set; }
        [DataMember] [BsonElement("MDB_Name")] public string Name { get; set; }
        [DataMember] [BsonElement("MDB_Kingdom")] public string Kingdom { get; set; }
        [DataMember] [BsonElement("MDB_Class")] public string Class { get; set; }
        [DataMember] [BsonElement("MDB_ConservationStatus")] public string ConservationStatus { get; set; }
        [DataMember] [BsonElement("MDB_Region")] public string Region { get; set; }
        [DataMember] [BsonElement("MDB_Extinct")] public bool Extinct { get; set; }
        [DataMember] [BsonElement("MDB_Birth")] public DateTime Birth { get; set; }
        [DataMember] [BsonElement("MDB_Death")] public DateTime Death { get; set; }
    }
}
