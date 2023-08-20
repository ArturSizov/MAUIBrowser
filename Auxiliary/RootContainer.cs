namespace MAUIBrowser.Auxiliary
{
    public class RootContainer
    {
        private static readonly object _lock = new();
        private static RootContainer? container = null;

        private IServiceProvider? serviceProvider;

        public static RootContainer Container
        {
            get
            {
                if (container == null)
                {
                    lock (_lock)
                    {
                        container ??= new RootContainer();
                    }
                }
                return container;
            }
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T Resolve<T>() where T : notnull
            => serviceProvider!.GetRequiredService<T>();
    }
}
