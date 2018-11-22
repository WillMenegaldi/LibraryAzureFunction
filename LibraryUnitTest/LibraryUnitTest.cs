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
        public async Task Test_SelectBookByID()
        {
            var bookDataAgentMock = new Mock<IBooksDataAgent>();

            var query = new Dictionary<string, StringValues>();
            string idBook = "0000000000001";
            query.Add("isbn", idBook);

            List<string[]> livro = new List<string[]>();

            bookDataAgentMock.Setup(x => x.Select(It.IsAny<string>()))
                .Returns(livro);

            var booksDataAgent = new BooksDataAgent();
            var result = await GetBooksFunction.Run(HttpRequestMock(query, null), bookDataAgentMock.Object, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }

        [Fact]
        public async Task Test_SelectAllBooks()
        {
            var bookDataAgentMock = new Mock<IBooksDataAgent>();

            List<string[]> livro = new List<string[]>();

            bookDataAgentMock.Setup(x => x.Select(It.IsAny<string>()))
                .Returns(livro);

            var booksDataAgent = new BooksDataAgent();
            var result = await GetBooksFunction.Run(HttpRequestMock(null, null), bookDataAgentMock.Object, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }

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

        [Fact]
        public async Task Test_DeleteBook()
        {
            var bookDataAgentMock = new Mock<IBooksDataAgent>();
            
            var query = new Dictionary<string, StringValues>();
            string idBook = "0000400000009";
            query.Add("isbn", idBook);

            bookDataAgentMock.Setup(x => x.ManipulationQuery(It.IsAny<string>()))
                .Returns("teste de unidade: delete");

            var result = await DeleteBooksFunction.Run(HttpRequestMock(query, null), bookDataAgentMock.Object, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }


        [Fact]
        public async Task Test_UpdateBook()
        {
            Books book = new Books(Isbn, Nmbook, Idauthor, Idpublisher)
            {
                Nmbook = "NAME_TESTUNIT",
                Idauthor = 49,
                Idpublisher = 49
            };

            var query = new Dictionary<string, StringValues>();
            string idBook = "0000000000009";
            query.Add("isbn", idBook);

            string serializedBook = JsonConvert.SerializeObject(book);
            var bookDataAgentMock = new Mock<IBooksDataAgent>();

            bookDataAgentMock.Setup(x => x.ManipulationQuery(It.IsAny<string>()))
                .Returns("UNIT TEST: UPDATE");

            var booksDataAgent = new BooksDataAgent();
            var result = await PutBookFunction.Run(HttpRequestMock(query, serializedBook), bookDataAgentMock.Object, log.Object);
            var resultObject = (ObjectResult)result;

            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}
