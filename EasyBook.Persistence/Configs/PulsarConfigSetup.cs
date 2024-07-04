using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EasyBook.Infrastructure.Configs
{
    public class PulsarConfigSetup: IConfigureOptions<PulsarConfig>
    {
        private readonly IConfiguration _configuration;

        public PulsarConfigSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(PulsarConfig options)
        {
            _configuration
                .GetSection(nameof(PulsarConfig))
                .Bind(options);
        }

    }
}
