namespace FilterDemo
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NotTransactionalAttribute : Attribute
    {
    }
}
