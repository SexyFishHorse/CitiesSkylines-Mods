namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;

    public class ServiceNotFoundException : Exception
    {
        public Type Service { get; private set; }

        public ServiceNotFoundException(Type service)
            : base(string.Format("No service registered for type \"{0}\".", service.FullName))
        {
            Service = service;
        }
    }
}
