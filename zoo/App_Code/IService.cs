using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Models;

namespace zoo
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllAnimals", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Animal> GetAllAnimals();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddAnimal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AddAnimal(string jsonAnimal);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllHouses", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<House> GetAllHouses();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllFiles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Node> GetAllFiles();
    }
}