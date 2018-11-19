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
    public static class PostBookFunction
    {
        [FunctionName("PostMethod")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Inject]IBooksDataAgent bookDataAgent,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Books book = JsonConvert.DeserializeObject<Books>(requestBody);
            
            string query = $"INSERT INTO BOOK (NMBOOK, IDAUTHOR, IDPUBLISHER, ISBN) VALUES('{book.Nmbook}', {book.Idauthor}, {book.Idpublisher}, '{book.Isbn}')";
            bookDataAgent.ManipulationQuery(query);
            return new OkObjectResult(book);
        }
    }
}
