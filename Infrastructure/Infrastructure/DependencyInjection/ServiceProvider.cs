namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class ServiceProvider : IServiceProvider
    {
        private static IServiceProvider instance;

        private readonly IDictionary<Type, object> serviceCache = new Dictionary<Type, object>();

        private readonly IDictionary<Type, ServiceDefinition> serviceDefinitions =
            new Dictionary<Type, ServiceDefinition>();

        public static IServiceProvider Instance
        {
            get
            {
                return instance ?? (instance = new ServiceProvider());
            }

            set
            {
                instance = value;
            }
        }

        public ILogger Logger { get; set; }

        public IServiceProvider Add(Type abstraction, object implementation)
        {
            try
            {
                if (abstraction.IsInstanceOfType(implementation) == false)
                {
                    throw new ArgumentException(
                        "The implementation " + implementation.GetType().FullName + " is not assignable to " +
                        abstraction.FullName);
                }

                serviceCache.Add(abstraction, implementation);

                return this;
            }
            catch (Exception ex)
            {
                if (Logger != null)
                {
                    Logger.LogException(ex);
                }

                throw;
            }
        }

        public IServiceProvider Add(Type abstraction, Type implementation, ServiceLifetime lifetime)
        {
            try
            {
                if (abstraction != implementation)
                {
                    if (abstraction.IsAssignableFrom(implementation) == false)
                    {
                        throw new ArgumentException(
                            "The implementation " + implementation.FullName + " is not assignable to " +
                            abstraction.FullName);
                    }
                }

                serviceDefinitions.Add(abstraction, new ServiceDefinition(implementation, lifetime));

                return this;
            }
            catch (Exception ex)
            {
                if (Logger != null)
                {
                    Logger.LogException(ex);
                }

                throw;
            }
        }

        public TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        public object GetService(Type type)
        {
            try
            {
                // See if the instance is already cached
                object service;
                if (serviceCache.TryGetValue(type, out service))
                {
                    return service;
                }

                // See if it has a definition
                ServiceDefinition serviceDefinition;
                if (serviceDefinitions.TryGetValue(type, out serviceDefinition))
                {
                    service = BuildService(serviceDefinition.InstanceType);

                    if (serviceDefinition.Lifetime == ServiceLifetime.Singleton)
                    {
                        serviceCache.Add(type, service);
                    }

                    return service;
                }
            }
            catch (Exception ex)
            {
                if (Logger != null)
                {
                    Logger.LogException(ex);
                }

                throw;
            }


            var exception = new ServiceNotFoundException(type);

            if (Logger != null)
            {
                Logger.LogException(exception);
            }

            throw exception;
        }

        private object BuildService(Type type)
        {
            var constructors = type.GetConstructors();
            var constructor = constructors.First();

            var paramTypes = constructor.GetParameters().Select(x => x.ParameterType);

            return Activator.CreateInstance(type, paramTypes.Select(GetService).ToArray());
        }
    }
}
