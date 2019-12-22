using System;

namespace Models
{
    public class Filters
    {
        public int priceMin { get; set; }
        public int priceMax { get; set; }
        public int sizeMin { get; set; }
        public int sizeMax { get; set; }
        public int minRoom { get; set; }
        public string city { get; set; }
    }
}
