using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using LibraryDataAgent.Interfaces;
using LibraryDataAgent.Models;
using LibraryDataAgent;
using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LibraryFunction
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class DeleteBooksFunction
    {
        [FunctionName("DeleteMethod")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            [Inject]IBooksDataAgent bookDataAgent,
            ILogger log)
        {
            string idBook = req.Query["isbn"];

            string query = $"DELETE FROM BOOK WHERE ISBN = {idBook}";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Books book = JsonConvert.DeserializeObject<Books>(requestBody);

            bookDataAgent.ManipulationQuery(query);
            return new OkObjectResult(book);
        }
    }
}
