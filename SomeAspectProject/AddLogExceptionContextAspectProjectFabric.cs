using iFOREX.Utilities.Logging.ExceptionContext.Ignore;
using Metalama.Framework.Code;
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
                .Where(t => t.TypeKind != TypeKind.Delegate)
                .SelectMany(t => t.Methods)
                .Where(m => !m.IsAbstract
                            && !MustIgnore(m.Attributes)))
                .AddAspectIfEligible<SomeAspect>();
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
