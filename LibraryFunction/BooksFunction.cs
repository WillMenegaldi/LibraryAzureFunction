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
    public static class BooksFunction
    {
        [FunctionName("GetMethod")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Inject]IBooksDataAgent bookDataAgent,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string book = req.Query["idbook"];

            string select = $"SELECT * FROM BOOK WHERE ISBN = {book}";
            string selectAll = "SELECT * FROM BOOK";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            book = book ?? data?.name;

            List<string[]> livro = new List<string[]>();

            if (book != null)
            {
                livro = bookDataAgent.Select(select);
            } else
            {
                livro = bookDataAgent.Select(selectAll);
            }

            return new OkObjectResult(livro);
        }
    }
}
