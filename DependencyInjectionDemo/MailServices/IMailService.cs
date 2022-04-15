namespace MailServices
{
    public interface IMailService
    {
        void Send(string title, string to, string body);
    }
}
