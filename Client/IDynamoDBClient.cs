using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.DataBase;
using TestTask.Responce;
namespace TestTask.Client
{
    public interface IDynamoDBClient
    {
        public Task<DB> GetAn(GetResponce get);
        public Task<DB> GetList(/*string sort,string by*/);
        public Task<List<itemsort>> sortAsync(string sort,string by,int page);
        public Task<bool> PostAn(PostParameter post);



    }
}
