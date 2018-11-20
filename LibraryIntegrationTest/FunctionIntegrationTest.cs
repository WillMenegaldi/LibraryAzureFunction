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

        [Fact]
        public async Task Test_GetBooks()
        {

        }
    }
}

