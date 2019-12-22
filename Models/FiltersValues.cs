using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class FiltersValues
    {
        public int priceMin { get; set; }
        public int priceMax { get; set; }
        public int sizeMin { get; set; }
        public int sizeMax { get; set; }
        public int minRoom { get; set; }
        public string city { get; set; }
    }
}
