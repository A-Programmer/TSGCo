using System.Threading;
using System.Threading.Tasks;
using Project.Auth.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Project.Auth.Services
{
    public class TokenCleanupHost : IHostedService
    {
        private readonly TokenCleanup _tokenCleanup;
        private readonly IOptions<OperationalStoreOptions> _options;

        public TokenCleanupHost(TokenCleanup tokenCleanup, IOptions<OperationalStoreOptions> options)
        {
            _tokenCleanup = tokenCleanup;
            _options = options;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_options.Value.EnableTokenCleanup)
            {
                _tokenCleanup.Start(cancellationToken);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_options.Value.EnableTokenCleanup)
            {
                _tokenCleanup.Stop();
            }

            return Task.CompletedTask;
        }
    }
}