using ConfigServices;
using LogServices;
using System;

namespace MailServices
{
    public class MailService : IMailService
    {
        private readonly ILogProvider logProvider;

        //private readonly IConfigService configService;

        //public MailService(
        //    ILogProvider logProvider, IConfigService configService)
        //{
        //    this.logProvider = logProvider;
        //    this.configService = configService;
        //}

        private readonly IConfigReader configReader;

        public MailService(
            ILogProvider logProvider, IConfigReader configReader)
        {
            this.logProvider = logProvider;
            this.configReader = configReader;
        }

        public void Send(string title, string to, string body)
        {
            this.logProvider.LogInfo("Ready to send");
            string server = this.configReader.GetValue("SmtpServer");
            string user = this.configReader.GetValue("UserName");
            string password = this.configReader.GetValue("Password");
            Console.WriteLine($"Server: {server}, User: {user}, Password: {password}");
            Console.WriteLine($"Mail Sending: {title}, {to}");
            this.logProvider.LogInfo("Finish");
        }
    }
}
