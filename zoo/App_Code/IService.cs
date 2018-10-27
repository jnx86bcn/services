﻿using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Models;

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
        void AddItem(Animal animal);
    }
}