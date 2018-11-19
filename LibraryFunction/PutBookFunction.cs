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
    public static class PutBookFunction
    {
        [FunctionName("PutMethod")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            [Inject]IBooksDataAgent bookDataAgent,
            ILogger log)
        {
            string idBook = req.Query["isbn"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Books book = JsonConvert.DeserializeObject<Books>(requestBody);

            string query = $"UPDATE BOOK SET NMBOOK = '{book.Nmbook}', IDAUTHOR = {book.Idauthor}, IDPUBLISHER = {book.Idpublisher} WHERE ISBN = '{idBook}'";

            bookDataAgent.ManipulationQuery(query);
            return new OkObjectResult(book);
            
        }
    }
}
