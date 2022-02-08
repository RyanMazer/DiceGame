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
using System.Data.SqlClient;

namespace DiceList
{
    public static class DeleteDiceList
    {
        [FunctionName("DeleteDice")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "post", Route = "deletedice/")] HttpRequest req,
             ILogger log)
        { 
            var connStr = Environment.GetEnvironmentVariable("SQLConnectionString");
            //Seems to not work on a consumption subscription
            //var passw = Environment.GetEnvironmentVariable("SecretsPassword", EnvironmentVariableTarget.Process);

            string passw = "TestingDice"; 

            if (!req.Headers.ContainsKey("passw"))
                return new ConflictObjectResult("Missing header passw");

            if (req.Headers["passw"] != passw)
                return new ConflictObjectResult("Bad password"); 

            var body = await req.ReadFormAsync();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                var command = "delete from Dice where DiceName in (";

                int i  = 0;
                foreach (var item in body)
                {
                    i++;
                    command += "'" + item.Key + "'";

                    if (body.Keys.Count < i)
                    {
                        command +=  ","; 
                    }
                }

                command += ")"; 

                SqlCommand comm = new SqlCommand(command, conn);
                
                int rows = comm.ExecuteNonQuery();

                if (rows == body.Count)
                    return new OkObjectResult("Sucess");
                else if (rows > 0)
                    return new ConflictObjectResult("1 or more rows were not deleted");
                else
                    return new ConflictObjectResult("No rows were effected at all");
            }
        }
    }
}

