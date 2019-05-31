using System;

namespace Models
{
    public class Node
    {
        public string NodeName { get; set; }
        public string NodeParent { get; set; }
        public bool isDocument { get; set; }
        public string PathUrl { get; set; }
        public string LinkFile { get; set; }
    }
}
