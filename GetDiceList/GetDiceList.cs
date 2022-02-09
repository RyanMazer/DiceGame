using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dice
{
    public static class GetDiceList
    {
        [FunctionName("GetDiceList")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetDiceList")] HttpRequest req,
    ILogger log,
    [Sql("select * from Dice",
    CommandType = System.Data.CommandType.Text,
    ConnectionStringSetting = "SqlConnectionString")]
    IEnumerable<DiceList> Dice)
        {
            return new OkObjectResult(Dice);
        }
    }
}
