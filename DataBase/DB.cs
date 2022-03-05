using System;
using System.Collections.Generic;
namespace TestTask.DataBase
{
    public class DB
    {
        public IEnumerable<Item> items  { get; set; }
    }
    public class Item
    {
       public string ID { get; set; }
        public string Name_ { get; set; }
        public string Desc_ { get; set; }
        public List<string> Image_ { get; set; }
        public string Price_ { get; set; }
        public string Date_ { get; set; }
        public string User_ { get; set; }


    }
 
    public class itemsort
    {

        public string ID { get; set; }
        public string Name_ { get; set; }
        public string Desc_ { get; set; }
        public List<string> Image_ { get; set; }
        public string Price_ { get; set; }
        public DateTime Date_ { get; set; }
        public string User_ { get; set; }


    }
}
