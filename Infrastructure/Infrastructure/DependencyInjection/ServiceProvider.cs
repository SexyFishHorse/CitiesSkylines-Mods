namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, object> serviceCache = new Dictionary<Type, object>();

        private readonly IDictionary<Type, ServiceDefinition> serviceDefinitions =
            new Dictionary<Type, ServiceDefinition>();

        public ILogger Logger { get; set; }

        public IServiceProvider Add(Type abstraction, object implementation)
        {
            try
            {
                if (abstraction.IsInstanceOfType(implementation) == false)
                {
                    throw new ArgumentException(string.Format("The implementation {0} is not assignable to {1}.", implementation.GetType().FullName, abstraction.FullName));
                }

                serviceCache.Add(abstraction, implementation);

                return this;
            }
            catch (Exception ex)
            {
                LogException(ex, "Error when trying to add implementation {0} of type {1}.", implementation, abstraction);

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
                            string.Format("The implementation {0} is not assignable to {1}.", implementation.FullName, abstraction.FullName));
                    }
                }

                serviceDefinitions.Add(abstraction, new ServiceDefinition(implementation, lifetime));

                return this;
            }
            catch (Exception ex)
            {
                LogException(ex, "Error occurred when adding an the implementation {0} as an abstraction of {1}.", implementation, abstraction);

                throw;
            }
        }

        public TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        public object GetService(Type type)
        {
            LogInfo("Requested type {0}.", type);

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
                LogException(ex, "Error occurred trying to instantiate an instance of {0}.", type);

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
            LogInfo("Building instance of {0}.", type);
            var constructors = type.GetConstructors();
            var constructor = constructors.First();

            var paramTypes = constructor.GetParameters().Select(x => x.ParameterType);

            return Activator.CreateInstance(type, paramTypes.Select(GetService).ToArray());
        }

        [StringFormatMethod("message")]
        private void LogInfo(string message, params object[] args)
        {
            if (Logger != null)
            {
                Logger.Info(message, args);
            }
        }

        [StringFormatMethod("message")]
        private void LogException(Exception ex, string message, params object[] args)
        {
            if (Logger != null)
            {
                Logger.Error(message, args);
                Logger.LogException(ex);
            }
        }
    }
}
