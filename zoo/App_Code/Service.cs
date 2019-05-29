using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using MongoDB.Driver;
using Models;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace zoo
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;

        public List<Animal> GetAllItems()
        {

            //Create client connection to our MongoDB database
            var client = new MongoClient(_connectionString);

            //Create a session object that is used when leveraging transactions
            var session = client.StartSession();

            //Create the collection object that represents the "products" collection
            IMongoCollection<Animal> animalsCollection = session.Client.GetDatabase("MongoDBStore").GetCollection<Animal>("animals");

            //Begin transaction
            session.StartTransaction();

            try
            {
                var filter = new FilterDefinitionBuilder<Animal>().Empty;
                List<Animal> results = animalsCollection.Find<Animal>(filter).ToList();

                //Made it here without error? Let's commit the transaction
                session.CommitTransaction();

                return results;
            }
            catch (Exception)
            {
                session.AbortTransaction();
                return null;
            }

        }

        public void AddItem(string json)
        {

            Animal animal = JsonConvert.DeserializeObject<Animal>(json);

            //Create client connection to our MongoDB database
            var client = new MongoClient(_connectionString);

            //Create a session object that is used when leveraging transactions
            var session = client.StartSession();

            //Create the collection object that represents the "products" collection
            var animals = session.Client.GetDatabase("MongoDBStore").GetCollection<Animal>("animals");

            //Begin transaction
            session.StartTransaction();

            try
            {
                //Insert the sample data 
                animals.InsertOneAsync(animal);

                //Made it here without error? Let's commit the transaction
                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.AbortTransaction();

            }

        }

        public List<FileInfo> GetAllFiles()
        {
            try
            {
                string path = HttpRuntime.AppDomainAppPath + "\\documents";
                DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
                List<FileInfo> files = new List<FileInfo>();
                IEnumerable<FileInfo> fileExt = d.GetFiles("*.pdf");
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.docx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.xlsx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.pptx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.txt", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                return files;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}