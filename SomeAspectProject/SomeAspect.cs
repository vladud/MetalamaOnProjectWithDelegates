using Metalama.Framework.Aspects;

namespace SomeAspect
{
    public class SomeAspect : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            //Do nothing
            return meta.Proceed();
        }
    }
}