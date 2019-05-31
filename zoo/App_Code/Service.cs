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

        public List<Node> GetAllFiles()
        {
            try
            {
                string path = HttpRuntime.AppDomainAppPath + "documents";
                string urlPath = HttpContext.Current.Request.Url.Scheme +":\\"+ HttpContext.Current.Request.Url.Host+"\\documents";
                DirectoryInfo d = new DirectoryInfo(path);
                List<Node> nodes = new List<Node>();
                List<FileInfo> files = new List<FileInfo>();
                IEnumerable<FileInfo> fileExt;
                fileExt = d.GetFiles("*.pdf", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.docx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.xlsx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.pptx", SearchOption.AllDirectories);
                files.AddRange(fileExt);
                fileExt = d.GetFiles("*.txt", SearchOption.AllDirectories);
                files.AddRange(fileExt);

                foreach(FileInfo file in files)
                {

                    string[] nodesName  = ("documents" + file.DirectoryName.Split(new[] { path }, StringSplitOptions.None)[1] + "\\" + file.Name).Split('\\');

                    //if (nodes.Count == 0)
                    //{
                    //    //root
                    //    Node node = new Node();
                    //    node.NodeName = nodesName[0];
                    //    node.isDocument = false;
                    //    node.NodeParent = "";
                    //    nodes.Add(node);
                    //}
                    
                    for (int i=1; i< nodesName.Length; i++)
                    {
                        if(nodes.FindIndex(r => r.NodeName.Equals(nodesName[i])) == -1 || nodes[nodes.FindIndex(r => r.NodeName.Equals(nodesName[i]))].isDocument == true)//new element
                        {
                            Node node = new Node();
                            node.NodeName = nodesName[i];
                            for(int j = 0; j<i; j++)
                            {
                                string[] Slice = new List<string>(nodesName).GetRange(0,i).ToArray();
                                node.NodeParent = "\\" + string.Join("\\", Slice);
                            }
                            if (file.Name == nodesName[i])
                            {
                                node.isDocument = true;
                                node.LinkFile = urlPath + file.DirectoryName.Split(new[] { path }, StringSplitOptions.None)[1] + "\\" + file.Name;
                                node.PathUrl = file.DirectoryName.Split(new[] { path }, StringSplitOptions.None)[1] + "\\" + file.Name;
                            }
                            else
                            {
                                node.isDocument = false;
                            }
                            nodes.Add(node);
                        }
                    }
                }

                return nodes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}