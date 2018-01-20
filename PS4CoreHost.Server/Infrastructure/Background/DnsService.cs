using DNS.Client.RequestResolver;
using DNS.Protocol;
using DNS.Protocol.ResourceRecords;
using DNS.Server;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PS4CoreHost.Server.Infrastructure.Background
{

    public class PSNetRequestResolver : IRequestResolver
    {
        public Task<IResponse> Resolve(IRequest request)
        {
            IResponse response = Response.FromRequest(request);

            var question = request.Questions.FirstOrDefault(x => x.Name.ToString().Contains("playstation.net"));

            if (question == null) return Task.FromResult<IResponse>(new Response());

            IResourceRecord record = new IPAddressResourceRecord(question.Name, IPAddress.Parse("192.168.0.254"));
            response.AnswerRecords.Add(record);
            return Task.FromResult(response);
        }
    }

    public class DnsHostedService : IHostedService, IDisposable
    {
        private DnsServer _server;

        public DnsHostedService()
        {
            _server = new DnsServer(new PSNetRequestResolver());
        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.Dispose();
                _server = null;
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _server.Requested += Server_Requested;
            _server.Listening += () => Console.WriteLine("DNS Listening...");

            await _server.Listen();
        }

        private void Server_Requested(IRequest request)
        {
            Console.WriteLine($"---> {request.Questions.First().Name}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _server.Dispose();
            return Task.CompletedTask;
        }
    }
}
