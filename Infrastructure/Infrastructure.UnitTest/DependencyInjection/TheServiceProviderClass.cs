namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.DependencyInjection
{
    using System;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using Xunit;

    [Trait("Category", "UnitTest")]
    public class TheServiceProviderClass
    {
        public class TheGetServiceMethod : TheServiceProviderClass
        {
            [Fact]
            public void ShouldThrowExceptionIfTypeIsNotRegistered()
            {
                var instance = new ServiceProvider();

                Action act = () => instance.GetService<Foo>();

                act.Should()
                    .Throw<ServiceNotFoundException>()
                    .WithMessage($"*{typeof(Foo).FullName}*")
                    .And.Service.Should().Be<Foo>();
            }

            [Fact]
            public void ShouldBuildServiceWithParameterlessConstructor()
            {
                var instance = new ServiceProvider();
                instance.AddTransient<Foo>();

                var service = instance.GetService<Foo>();

                service.Should().NotBeNull();
            }

            [Fact]
            public void ShouldReturnImplementationWhenAskingForAbstraction()
            {
                var instance = new ServiceProvider();
                instance.AddTransient<IFoo, Foo>();

                var service = instance.GetService<IFoo>();

                service.Should().NotBeNull().And.BeOfType<Foo>();
            }

            [Fact]
            public void ShouldBuildServiceThatHasAConstructorParameter()
            {
                var instance = new ServiceProvider();
                instance.AddTransient<FooWithBar>().AddTransient<Bar>();

                var service = instance.GetService<FooWithBar>();

                service.Should().NotBeNull();
            }

            [Fact]
            public void ShouldThrowExceptionIfDependencyIsNotAddedToTheProvider()
            {
                var instance = new ServiceProvider();
                instance.AddTransient<FooWithBar>();

                Action act = () => instance.GetService<FooWithBar>();

                act.Should().Throw<ServiceNotFoundException>().WithMessage("*"+typeof(Bar).FullName+"*");
            }
        }

        public class Foo : IFoo
        {
        }

        public interface IFoo
        {

        }

        public class Bar
        {
        }

        public class FooWithBar
        {
            public Bar Bar { get; set; }

            public FooWithBar(Bar bar) => Bar = bar;
        }
    }
}
