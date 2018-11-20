using LibraryDataAgent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryIntegrationTest
{
    public class FunctionTestBase
    {
        public HttpRequest HttpRequestMock(Dictionary<string, StringValues> query, string body, string token)
        {
            var reqMock = new Mock<HttpRequest>();

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            reqMock.Setup(req => req.Body).Returns(stream);
            if (!string.IsNullOrEmpty(token))
            {
                reqMock.Setup(req => req.Headers["Authorization"]).Returns(new StringValues(string.Concat("Static ", token)));
            }
            return reqMock.Object;
        }
    }
}
