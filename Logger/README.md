# CitiesSkylines-Logger
A friendly logger for Cities Skylines. Also allows for writing the log to a file.

![Appveyor](https://ci.appveyor.com/api/projects/status/github/sexyfishhorse/citiesskylines-logger?svg=true) [![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg?maxAge=2592000)](https://gitter.im/SexyFishHorse/gitter)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://sexyfishhorse.mit-license.org/)

# Nuget package
This project is available as a nuget package. Add the appropriate feed to your Nuget package source list ([how to](https://docs.nuget.org/consume/package-manager-dialog))
* Visual Studio 2015+: https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v3/index.json
* Visual Studio 2012+: https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v2

# Usage

```
// Add the using
using SexyFishHorse.CitiesSkylines.Logger

// Create the logger
var logger = new Logger("mod_folder_name", "log_file_name.xml", clearLog: false);

// Use the logger
logger.Log("Hello world");
logger.Warn("Something's fishy");
logger.Error("Aaaand it broke");
```

- `"mod_folder_name"` is the name of your mods folder in `%LOCALAPPDATA%\Colossal Order\CitiesSkylines\Addons\Mods` The log will be stored in here
- `log_file_name.xml` is the name of the log file
- `clearLog` If set to `true` existing log files are appended, `false` overwrites old log files

**Note:** You can have multiple loggers at work at the same time for various log files, but you should only have one logger per log file otherwise you may run into concurrency issues.

## Configuration

The logger contains some configuration possibilities available as properties on the Logger class.

Property|default|Description
--------|-------|-----------
`LogToFile` | `false` | Indicates if messages should be written to the log file
`LogToOutputPanel` | `false` | Indicates if messages should be written to the in-game output panel.<br/>**Note:** Errors are always written to the debug panel

## Installation

### Nuget feed details
This library is available as a nuget package on the SexyFishHorse-CitiesSkylines nuget feed:

Description | URL
------------|----
NuGet V3 feed url (Visual Studio 2015+) | `https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v3/index.json`
NuGet V2 feed url (Visual Studio 2012+) | `https://www.myget.org/F/sexyfishhorse-citiesskylines/api/v2`
Symbol server url | `https://www.myget.org/F/sexyfishhorse-citiesskylines/symbols/`

### Install command

`PM> Install-Package SexyFishHorse.CitiesSkylines.Logger`
