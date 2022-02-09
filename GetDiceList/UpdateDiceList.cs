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
    public static class UpdateDiceList
    {
        [FunctionName("UpdateDiceList")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateDiceList")] HttpRequest req,
[Sql("Dice", ConnectionStringSetting = "SqlConnectionString")] ICollector<DiceList> diceList)
        {
            var body = await req.ReadFormAsync();

            foreach (var i in body)
            {
                DiceList dice = new DiceList();
                dice.DiceName = i.Key;
                dice.DiceFace = i.Value; 

                diceList.Add(dice);
            }

            return new OkObjectResult("Done");
        }
    }
}
