using LibraryDataAgent;
using LibraryDataAgent.Interfaces;
using LibraryDataAgent.Models;
using LibraryFunction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private string _isbn;
        private int _idauthor;
        private int _idpublisher;
        private string _nmbook;

        [Fact]
        public async Task Test_InsertBooks()
        {
            Books book = new Books(_isbn, _nmbook, _idauthor, _idpublisher)
            {
                Isbn = "0000000000009",
                Nmbook = "TESTE",
                Idauthor = 100,
                Idpublisher = 100
            };

            string body = JsonConvert.SerializeObject(book);

            var bookDataAgent = new BooksDataAgent();
            var result = await PostBookFunction.Run(HttpRequestMock(null, body), bookDataAgent, log.Object);
            var resultObject = (ObjectResult)result;
            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}
