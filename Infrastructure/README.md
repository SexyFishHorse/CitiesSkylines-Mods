# CitiesSkylines-Infrastructure

Infrastructure code for Cities Skylines modding

[![Build status](https://ci.appveyor.com/api/projects/status/60s6mvfoqo665qmq/branch/master?svg=true)](https://ci.appveyor.com/project/asser-dk/citiesskylines-mods/branch/master)
[![License](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://sexyfishhorse.mit-license.org/)

# Installation

## Nuget feed details

This library is available as a nuget package on the SexyFishHorse-CitiesSkylines NuGet feed:

| Description                             | URL                                                                      |
|-----------------------------------------|--------------------------------------------------------------------------|
| NuGet V3 feed url (Visual Studio 2015+) | `https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v3/index.json` |
| NuGet V2 feed url (Visual Studio 2012+) | `https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v2`            |
| Symbol server url                       | `https://www.myget.org/F/sexyfishhorse-citiesskylines/symbols/`          |

## Install command

`PM> Install-Package SexyFishHorse.CitiesSkylines.Infrastructure`

# Features

## Dependency Injection

Use the ServiceProvider to make handling dependencies easier.

Imagine you have a class structure like this:

Class `Foo` that inherits from `IFoo`, and has a property of type `Bar`

```c#
public class Foo : IFoo
{
    public Foo(Bar bar)
    {
        Bar = bar
    }

    public Bar Bar { get; private set; }
}

public interface IFoo
{
    Bar Bar { get; }
}

public class Bar
{
}
```

Set up the service provider like so:

```c#
ServiceProvider.Instance
    .AddSingleton<IFoo, Foo>() // Add the Foo class so when
    .AddSingleton<Bar>();
```

> **Note:** `Singleton` means that when you request the service from the provider you will always get the exact same object. You can add services as `Transient` instead which means that every time you ask for a service you get a new object.

Request services like so:

```c#
var foo = ServiceProvider.Instance.GetService<IFoo>();
```

## Logging

```c#
// Create the logger
var logger = new Logger("mod_name");

// Use the logger
logger.Log("Hello world");
logger.Warn("Something's fishy");
logger.Error("Aaaand it broke");
logger.LogException(new Exception("Something exploded"));
```

> **Note:** Logs will be stored in the `%LOCALAPPDATA%\Colossal Order\CitiesSkylines\Addons\Mods\{mod_name}` folder.

## Configuration

The logger contains some configuration possibilities available as properties on the Logger class.

| Property            | Default | Description                                                                                                                     |
|---------------------|---------|---------------------------------------------------------------------------------------------------------------------------------|
| `LogToFile`         | `false` | Indicates if messages should be written to the log file                                                                         |
| `LogToOutputPanel`  | `false` | Indicates if messages should be written to the in-game output panel.<br/>**Note:** Errors are always written to the debug panel |

