using System.Collections.Generic;

namespace ConfigServices
{
    public class LayeredConfigReader : IConfigReader
    {
        private readonly IEnumerable<IConfigService> configServices;

        public LayeredConfigReader(IEnumerable<IConfigService> configServices)
        {
            this.configServices = configServices;
        }

        public string GetValue(string name)
        {
            string value = string.Empty;

            foreach (var service in this.configServices)
            {
                string newValue = service.GetValue(name);
                if (!string.IsNullOrEmpty(newValue))
                {
                    value = newValue;
                }
            }

            return value;
        }
    }
}
