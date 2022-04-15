namespace ConfigServices
{
    public interface IConfigReader
    {
        string GetValue(string name);
    }
}
