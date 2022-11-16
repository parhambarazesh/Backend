using System;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace QuizClient.Tests
{
    public class QuizServiceApiPact : IDisposable
    {
        public IMockProviderService MockProviderService { get; }
        public int MockServerPort => 9222;
        public Uri MockProviderServiceBaseUri => new Uri($"http://localhost:{MockServerPort}");

        public QuizServiceApiPact()
        {
            PactBuilder pactBuilder = new PactBuilder(new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"..\..\..\..\pacts",
                LogDir = @"..\..\..\..\logs"
            });
            pactBuilder.ServiceConsumer("QuizClient").HasPactWith("QuizService");
            MockProviderService = pactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            MockProviderService.ClearInteractions();
        }
    }
}