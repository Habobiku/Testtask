using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Client;
using TestTask.DataBase;
using TestTask.Models;
using TestTask.Responce;

namespace TestTask.Controllers
{
    [ApiController]
    [Route(@"api")]
    public class DBController : Controller
    {

        private readonly IDynamoDBClient _dynamoDbClient;
        private ILogger<DBController> _logger;

        public DBController(ILogger<DBController> logger, IDynamoDBClient dynamoDBClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDBClient;



        }
        [HttpGet("GetAn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetAn([FromQuery] GetResponce get)
        {
            var result = await _dynamoDbClient.GetAn(get);
            if (result == null)
                return NotFound("Not found ad");
            
            
           
       
            return Ok(result);

        }


        [HttpGet("GetList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetList([FromQuery] string sort,string by,int page)
        {

            var result = await _dynamoDbClient.sortAsync(sort,by,page);
            if (result == null)
                return NotFound("Error");

            return Ok(result);
           

        }
        

        [HttpPost("add")]
        public async Task<IActionResult> PostAn([FromQuery] Post post)
        {

            var data = new PostParameter
            {
                ID = Guid.NewGuid().ToString(),
                Name = post.Name,
                Date = post.Date,
                Desc = post.Desc,
                Image = post.Image,
                Price = post.Price,
                User = post.User
            };

            var result = await _dynamoDbClient.PostAn(data);

            if (result == false)
                return BadRequest("Cannot insert to database");

            return Ok("Successful have been added.Your id=" + data.ID);


        }




    }
}
