using System;

namespace ConfigServices
{
    public class EnvironmentConfigService : IConfigService
    {
        public string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
