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
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllItems", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Animal> GetAllItems();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddItem/{Name}/{Kingdom}/{Class}/{ConservationStatus}/{Region}/{Extinct}/{Birth}/{Death}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void AddItem(string Name,string Kingdom,string Class,string ConservationStatus,string Region,string Extinct,string Birth,string Death);
    }
}
