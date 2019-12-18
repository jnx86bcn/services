using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Models;

namespace MongoDBTransaction
{
    class Program
    {

        const string MongoDBConnectionString = "mongodb+srv://admin:1234@cluster0-5mgk3.azure.mongodb.net/test?retryWrites=true&w=majority";
        
        static void Main(string[] args)
        {
            //UpdateAnimals();
            //UpdateHouses();
            //Console.WriteLine("Finished updating the product collection");
            //Console.ReadKey();
        }
        static void UpdateAnimals()
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
            var basicModel = new Animal
            {
                Name = "",
                Kingdom = "",
                Class = "",
                ConservationStatus = "",
                Region = "",
                Extinct = false,
                Birth = new DateTime(),
                Death = new DateTime(),
            };

            //Begin transaction
            session.StartTransaction();

            try
            {
                //Insert the sample data 
                animals.InsertOneAsync(basicModel);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }

        }

        static void UpdateHouses()
        {
            //Create client connection to our MongoDB database
            var client = new MongoClient(MongoDBConnectionString);

            //Create a session object that is used when leveraging transactions
            var session = client.StartSession();

            //Create the collection object that represents the "products" collection
            var houses = session.Client.GetDatabase("MongoDBStore").GetCollection<House>("houses");

            //Clean up the collection if there is data in there
            houses.Database.DropCollection("houses");

            //Create some sample data
            var basicModel = new House
            {
                Title = "",
                UrlPhoto = "",
                City = "",
                Price = 0,
                SQM = 0,
                Rooms = 0,
                Bathrooms = 0
            };

            //Begin transaction
            session.StartTransaction();

            try
            {
                //Insert the sample data 
                houses.InsertOneAsync(basicModel);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                session.AbortTransaction();
            }

        }
    }
}
