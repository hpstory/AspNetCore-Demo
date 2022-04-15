using Microsoft.Extensions.Options;

namespace ConfigurationDemo
{
    class ConfigController
    {
        private readonly IOptionsSnapshot<Config> optionsConfig;

        public ConfigController(IOptionsSnapshot<Config> optionsConfig)
        {
            this.optionsConfig = optionsConfig;
        }

        public void Read()
        {
            Console.WriteLine(this.optionsConfig.Value.Name);
            Console.WriteLine(this.optionsConfig.Value.Age);
        }
    }
}
