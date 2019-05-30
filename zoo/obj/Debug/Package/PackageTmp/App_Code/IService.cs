using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Models;
using System.IO;

namespace zoo
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllItems", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Animal> GetAllItems();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AddItem(string json);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllFiles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<CustomFileInfo> GetAllFiles();
    }
}
