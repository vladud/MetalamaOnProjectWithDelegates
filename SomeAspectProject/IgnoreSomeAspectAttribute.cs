namespace iFOREX.Utilities.Logging.ExceptionContext.Ignore
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class IgnoreSomeAspectAttribute : Attribute
    {
    }
}
