using LibraryDataAgent;
using LibraryDataAgent.Interfaces;
using LibraryDataAgent.Models;
using LibraryFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace LibraryIntegrationTest
{
    public class FunctionIntegrationTest : FunctionTestBase
    {
        readonly Mock<ILogger> log = new Mock<ILogger>();

        public string Isbn { get; set; }
        public string Nmbook { get; set; }
        public int Idauthor { get; set; }
        public int Idpublisher { get; set; }

        [Fact]
        public async Task Test_SelectAllBooks()
        {
            var bookDataAgent = new BooksDataAgent();
            IActionResult result = await GetBooksFunction.Run(HttpRequestMock(null, null), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;
            
            Assert.Equal(200, resultObject.StatusCode);
        }
        
        [Fact]
        public async Task Test_SelectBookByID()
        {
            var query = new Dictionary<string, StringValues>();
            string idBook = "0000000000001";
            query.Add("isbn", idBook);

            var bookDataAgent = new BooksDataAgent();
            IActionResult result = await GetBooksFunction.Run(HttpRequestMock(query, null), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }
        
        [Fact]
        public async Task Test_InsertBooks()
        {
            Books book = new Books(Isbn, Nmbook, Idauthor, Idpublisher)
            {
                Isbn = "ISBN_TESTTEST",
                Nmbook = "TESTE",
                Idauthor = 10,
                Idpublisher = 10
            };

            string body = JsonConvert.SerializeObject(book);

            var bookDataAgent = new BooksDataAgent();
            IActionResult result = await PostBookFunction.Run(HttpRequestMock(null, body), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteBooks()
        {
            var query = new Dictionary<string, StringValues>();
            string idBook = "0000400000009";
            query.Add("isbn", idBook);

            var bookDataAgent = new BooksDataAgent();
            IActionResult result = await DeleteBooksFunction.Run(HttpRequestMock(query, null), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Test_UpdateBooks()
        {
            Books book = new Books(Isbn, Nmbook, Idauthor, Idpublisher)
            {
                Nmbook = "NAME_TESTUNIT",
                Idauthor = 49,
                Idpublisher = 49
            };

            string body = JsonConvert.SerializeObject(book);
            var query = new Dictionary<string, StringValues>();
            string idBook = "0000000000009";
            query.Add("isbn", idBook);

            var bookDataAgent = new BooksDataAgent();
            IActionResult result = await PutBookFunction.Run(HttpRequestMock(query, body), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}
