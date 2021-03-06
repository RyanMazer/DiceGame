using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace Dice
{
    public static class CloseSession
    {
        [FunctionName("CloseSession")]
        public static async Task<IActionResult> Run(
                     [HttpTrigger(AuthorizationLevel.Function, "post", Route ="CloseSession")] HttpRequest req,
                     ILogger log)
        {
            var connStr = Environment.GetEnvironmentVariable("SQLConnectionString");

            var body = await req.ReadFormAsync();

            if (!body.ContainsKey("ip"))
                return new ConflictObjectResult("Missing body");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string Ip = body["ip"];

                var command = string.Format($"delete from Sessions where Ip = '{Ip}'");

                SqlCommand comm = new SqlCommand(command, conn);

                int rows = comm.ExecuteNonQuery();

                if (rows == 1)
                    return new OkObjectResult("Sucess");
                else
                    return new ConflictObjectResult("No rows were effected at all");
            }
        }
    }
}
