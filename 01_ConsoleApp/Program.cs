using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTransaction
{
    class Program
    {
        public class Animal
        {
            [DataMember] [BsonId] public ObjectId Id { get; set; }
            [DataMember] [BsonElement("MDB_Name")] public string Name { get; set; }
            [DataMember] [BsonElement("MDB_Kingdom")] public string Kingdom { get; set; }
            [DataMember] [BsonElement("MDB_Class")] public string Class { get; set; }
            [DataMember] [BsonElement("MDB_ConservationStatus")] public string ConservationStatus { get; set; }
            [DataMember] [BsonElement("MDB_Region")] public string Region { get; set; }
            [DataMember] [BsonElement("MDB_Extinct")] public bool Extinct { get; set; }
            [DataMember] [BsonElement("MDB_Birth")] public DateTime Birth { get; set; }
            [DataMember] [BsonElement("MDB_Death")] public DateTime Death { get; set; }
        }


        const string MongoDBConnectionString = "mongodb+srv://admin:1234@cluster0-5mgk3.azure.mongodb.net/test? retryWrites=true";
        

        static void Main(string[] args)
        {
            UpdateProducts();
            Console.WriteLine("Finished updating the product collection");
            Console.ReadKey();
        }
        static void UpdateProducts()
        {
            //Create client connection to our MongoDB database
            var client = new MongoClient(MongoDBConnectionString);

            //Create a session object that is used when leveraging transactions
            var session = client.StartSession();

            //Create the collection object that represents the "products" collection
            var animals = session.Client.GetDatabase("MongoDBStore").GetCollection<Animal>("animals");

            //Clean up the collection if there is data in there
            animals.Database.DropCollection("animals");

            //Create some sample data
            var tiger = new Animal {
                Name = "Elephant",
                Kingdom = "A",
                Class = "asdfas",
                ConservationStatus= "agwewert",
                Region = "sdfhsdhrs",
                Extinct = false,
                Birth = new DateTime(),
                Death = new DateTime(),
            };

            //Begin transaction
            session.StartTransaction();

            try
            {
                //Insert the sample data 
                animals.InsertOneAsync(tiger);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }
          
        }
    }
}
