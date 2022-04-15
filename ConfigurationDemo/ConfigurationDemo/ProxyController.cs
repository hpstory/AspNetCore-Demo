using Microsoft.Extensions.Options;

namespace ConfigurationDemo
{
    class ProxyController
    {
        private readonly IOptionsSnapshot<Proxy> optionsConfig;

        public ProxyController(IOptionsSnapshot<Proxy> optionsConfig)
        {
            this.optionsConfig = optionsConfig;
        }

        public void Read()
        {
            Console.WriteLine(this.optionsConfig.Value.Address);
            Console.WriteLine(this.optionsConfig.Value.Ports[0]);
        }
    }
}
