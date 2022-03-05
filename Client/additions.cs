using System;
using TestTask.Models;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using TestTask.DataBase;
using System.Linq;
using System.Globalization;

namespace TestTask.Client
{
    public static class Additions
    {
        
       
        public static ScanRequest scaning(string key, string ID)
        {

            if(string.IsNullOrEmpty(key)&& string.IsNullOrEmpty(ID))
            {
                return new ScanRequest
                {
                    TableName = config.anTable,
                };
            }
           
                return new ScanRequest
            {
                
                TableName = config.anTable,
                
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {
                    ":v_"+key,new AttributeValue{S=ID} }
                },
                FilterExpression = $"{key}=:v_{key}",

            };
           
           

        }
        public static List<itemsort> Sort(DB db,string sort,string by,int page)
        {
            List<itemsort> lol = new List<itemsort>();
            List<itemsort> sorted = new List<itemsort>();
            foreach (var i in db.items)
            {
                var res = new itemsort
                {
                    Date_ = Convert.ToDateTime(i.Date_),
                    //Convert.ToDateTime(i.Date_, "yyyy-MM-dd"),
                    Desc_ = i.Desc_,
                    ID = i.ID,
                    Image_ = i.Image_,
                    Name_ = i.Name_,
                    Price_ = i.Price_,
                    User_ = i.User_,
                };
                lol.Add(res);
                
            }
          
            if (sort == "Date")
            {   
                if(by== "Desc")
               sorted = lol.OrderByDescending(x => x.Date_).Skip((page-1)*10).Take(10).ToList();
                if(by== "Asc")
                    sorted = lol.OrderBy(x => x.Date_).Skip((page - 1) * 10).Take(10).ToList();

            }
            if (sort == "Price")
            {
                if (by == "Desc")
                    sorted = lol.OrderByDescending(x => x.Price_).Skip((page - 1) * 10).Take(10).ToList();
                if (by == "Asc")
                    sorted = lol.OrderBy(x => x.Price_).Skip((page - 1) * 10).Take(10).ToList();
            }
            return sorted;
        }   
        //public static QueryRequest query(/*string sort, string by*/)
        //{
        //    return new QueryRequest
        //    {
        //        Limit=10,
        //        TableName=config.anTable,
        //        ScanIndexForward=true,
        //        ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
        //        {
        //            ":v_ID",new AttributeValue{S=""} }
        //        },
        //        KeyConditionExpression ="ID=:v_ID",

        

        //    };

        //}


    }
}
