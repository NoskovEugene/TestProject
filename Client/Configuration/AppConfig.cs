using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Configuration
{
    public class AppConfig : IAppConfig
    {
        private IOptions<ColorConfig> colorConfig;
        private IOptions<NetworkConfiguration> networkConfig;

        public AppConfig(IOptions<ColorConfig> colors, IOptions<NetworkConfiguration> networkConfig)
        {
            colorConfig = colors;
            this.networkConfig = networkConfig;
        }

        public ColorConfig ColorConfig => colorConfig.Value;

        public NetworkConfiguration NetworkConfig => networkConfig.Value;
    }
}
