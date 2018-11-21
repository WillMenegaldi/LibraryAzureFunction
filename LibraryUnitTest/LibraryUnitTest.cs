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

namespace LibraryUnitTest
{
    public class LibraryUnitTest : FunctionTestBase
    {
        readonly Mock<ILogger> log = new Mock<ILogger>();

        public string Isbn { get; set; }
        public string Nmbook { get; set; }
        public int Idauthor { get; set; }
        public int Idpublisher { get; set; }

        [Fact]
        public async Task Test_InsertBook()
        {
            Books book = new Books(Isbn, Nmbook, Idauthor, Idpublisher)
            {
                Isbn = "ISBN_TESTTEST",
                Nmbook = "NAME_TEST",
                Idauthor = 100,
                Idpublisher = 100
            };

            string serializedBook = JsonConvert.SerializeObject(book);
            var bookDataAgentMock = new Mock<IBooksDataAgent>();

            List<string[]> livro = new List<string[]>();

            bookDataAgentMock.Setup(x => x.ManipulationQuery(It.IsAny<string>()))
                .Returns("teste");

            var booksDataAgent = new BooksDataAgent();
            var result = await PostBookFunction.Run(HttpRequestMock(null, serializedBook), bookDataAgentMock.Object, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}