using iFOREX.Utilities.Logging.ExceptionContext.Ignore;
using Metalama.Framework.Code.Collections;
using Metalama.Framework.Fabrics;

namespace SomeAspect
{
    public class AddSomeAspectProjectFabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            amender
                .With(p =>
                p
                .Types
                .SelectMany(t => t.Methods)
                .Where(m => !m.IsAbstract
                            && !MustIgnore(m.Attributes)))
                .AddAspect<SomeAspect>();
        }

        private bool MustIgnore(IAttributeCollection attributes)
        {
            return attributes.Any(a =>
            {
                try
                {
                    return a.Type.ToType() == typeof(IgnoreSomeAspectAttribute);
                }
                catch
                {
                    return true;
                }
            });
        }
    }
}
