using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dice
{
    public static class RegisterSession
    {
        [FunctionName("RegisterSession")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "RegisterSession")] HttpRequest req,
[Sql("Sessions", ConnectionStringSetting = "SqlConnectionString")] ICollector<Session> Session)
        {
            var body = await req.ReadFormAsync();

            Session sess = new Session();

            if (body.Keys.Contains("name") && body.Keys.Contains("ip"))
            {
                sess.Name = body["name"];
                sess.Ip = body["ip"];
            }
            else
                return new ConflictObjectResult("Missing Body name or ip");

            if (body.Keys.Contains("pass"))
                sess.Password = body["pass"];
            
            Session.Add(sess); 

            return new OkObjectResult("Done");
        }
    }
}
