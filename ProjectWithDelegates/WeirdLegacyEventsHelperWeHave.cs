using iFOREX.Utilities.Logging.ExceptionContext.Ignore;
using System.ComponentModel;

namespace MetalamaOnProjectWithDelegates
{
    public delegate void GenericEventHandler();
    public delegate void GenericEventHandler<T>(T t);

    [IgnoreSomeAspectAttribute]
    public static class WeirdLegacyEventsHelperWeHave
    {
        [IgnoreSomeAspectAttribute]
        public static void Fire(GenericEventHandler del)
        {
            UnsafeFire(del);
        }

        [IgnoreSomeAspectAttribute]
        public static void Fire<T>(GenericEventHandler<T> del, T t)
        {
            UnsafeFire(del, t);
        }

        [IgnoreSomeAspectAttribute]
        public static void UnsafeFire(Delegate del, params object[] args)
        {
            Delegate[] delegates = del.GetInvocationList();

            foreach (Delegate sink in delegates)
            {
                try
                {
                    InvokeDelegate(sink, args);
                }
                catch
                {
                    // ignored
                }
            }
        }

        [IgnoreSomeAspectAttribute]
        private static void InvokeDelegate(Delegate del, object[] args)
        {
            ISynchronizeInvoke synchronizer = del.Target as ISynchronizeInvoke;
            if (synchronizer != null)
            {
                if (synchronizer.InvokeRequired)
                {
                    synchronizer.Invoke(del, args);
                    return;
                }
            }
            del.DynamicInvoke(args);
        }
    }
}