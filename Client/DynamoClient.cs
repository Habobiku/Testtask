using System;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.DataBase;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using TestTask.Extentions;
using TestTask.Responce;
using System.Net.Http;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using System.Linq;

namespace TestTask.Client
{
    public class DynamoClient : IDynamoDBClient
    {
        public readonly string _Table;
        private readonly IAmazonDynamoDB _dynamoDb;
   
        public DynamoClient(IAmazonDynamoDB dynamoDB)
        {
            _Table = config.anTable;
            _dynamoDb = dynamoDB;
    
        }

        public async Task<DB> GetAn(GetResponce get)
        {

            

            var responce = await _dynamoDb.ScanAsync(Additions.scaning(get.key,get.id));

              if (responce.Items.Count == 0)
                return null;
            var lastKeyEvaluated = responce.LastEvaluatedKey;
            return new DB
            {
                items = responce.Items.Select(Extension.Map),
            };
            
        }
      


        public async Task<DB> GetList(/*string sort,string by*/)
        {


            var responce = await _dynamoDb.ScanAsync(Additions.scaning(null,null)) ;
            
            return new DB
            {
                items = responce.Items.Select(Extension.Map),
            };
             


        }

        public async Task<List<itemsort>> sortAsync(string sort,string by,int page)
        {

            var resp = await GetList();
           var res= Additions.Sort(resp,sort,by,page);

            if (res.Count == 0)
             return null; 

            if(res.Count<10)
            return res;
            else
            {

                return res;
            }
           


        }




        public async Task<bool> PostAn(PostParameter data)
        {
            var request = new PutItemRequest
            { 
                TableName = _Table,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"ID", new AttributeValue{S=data.ID } },
                    {"Date_", new AttributeValue{S=data.Date} },
                    {"Desc_", new AttributeValue{S=data.Desc} },
                    {"Image_", new AttributeValue{SS=data.Image} },
                    {"Name_", new AttributeValue{S=data.Name} },
                    {"Price_", new AttributeValue{S=data.Price}},
                    {"User_", new AttributeValue{S=data.User} },
                },
                
                
            };
            try
            {
                var responce = await _dynamoDb.PutItemAsync(request);
                
                return responce.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is mistake" + e);
                return false;
            }

        }

    }
}

