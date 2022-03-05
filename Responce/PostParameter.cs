using System;
using System.Collections.Generic;

namespace TestTask.Responce
{
    public class PostParameter
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public  List <string> Image { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Desc { get; set; }
        public string User { get; set; }

    }
    public class Post
    {
        public string Name { get; set; }
        public List<string> Image { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Desc { get; set; }
        public string User { get; set; }
    }
}
