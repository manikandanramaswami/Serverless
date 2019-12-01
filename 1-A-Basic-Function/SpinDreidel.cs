using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Manix.Serverless
{
    public static class SpinDreidel
    {
        [FunctionName("SpinDreidel")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var randomNumber  = new Random();
            //נ (Nun), ג (Gimmel), ה (Hay), or ש (Shin)
            DreidelFace[] dreidel = new DreidelFace[4];
            dreidel[0] = new DreidelFace(){Face="נ",FaceText="Nun" };
            dreidel[1] = new DreidelFace(){Face="ג",FaceText="Gimmel" };
            dreidel[2] = new DreidelFace(){Face="ה",FaceText="Hay" };
            dreidel[3] = new DreidelFace(){Face="ש",FaceText="Shin" };
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            return (ActionResult)new OkObjectResult(dreidel[randomNumber.Next(dreidel.Length)]);
        }
    }
    public class DreidelFace
    {
        public string Face {get;set;}
        public string FaceText {get;set;}
    }
}
