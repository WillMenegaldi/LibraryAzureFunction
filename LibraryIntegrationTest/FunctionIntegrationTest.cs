/*using LibraryDataAgent;
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
            AbrirOcorrenciaRequest abrirOcorrenciaRequest = new AbrirOcorrenciaRequest()
            {
                IdCliente = "3703CCC4-BC5D-DE11-B2C7-001B78B9D994",
                MensagemUsuario = "Quando vou poder pegar as minhas chaves?",
                IdUnidade = "BAC5C28D-0710-DE11-8C23-001B78B9D994",
                MotivoContato = "Motivo1",
                Ocorrencias = new List<Ocorrencia>()
                {
                    new Ocorrencia()
                    {
                        Email = "Este é o texto q deve ser enviado por e-mail",
                        IdAssuntoOcorrencia = "DA0D577A-3A9A-E611-ABB6-80C16E075108",
                        Resposta = "Sua chave será entregue logo logo!",
                        RespostaEspecifica = true,
                        TituloResposta = "Entrega de chaves"
                    }
                }
            };

            string body = JsonConvert.SerializeObject(abrirOcorrenciaRequest);

            var crmDataAgent = new CrmDataAgent();
            var result = await AbrirOcorrenciaFaleConosco.Run(HttpRequestMock(null, body, _token), crmDataAgent, log.Object);
            var resultObject = (ObjectResult)result;
            Assert.Equal(200, resultObject.StatusCode);
            AbrirOcorrenciaResponse response = (AbrirOcorrenciaResponse)resultObject.Value;
            Assert.False(response.AdicionarInformacoesExtras);
            Assert.Null(response.IdChamado);
        }
    }
}
*/