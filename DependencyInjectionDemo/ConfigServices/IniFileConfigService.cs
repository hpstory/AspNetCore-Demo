using System.IO;
using System.Linq;

namespace ConfigServices
{
    class IniFileConfigService : IConfigService
    {
        public string? FilePath { get; set; }

        public string GetValue(string name)
        {
            var pair = File.ReadAllLines(FilePath).Select(line => line.Split('='))
                .Select(str => new { Key = str[0], Value = str[1] })
                .SingleOrDefault(k => k.Key == name);
            if (pair != null)
            {
                return pair.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
