# Cities Skylines Mods

This repository contains all my mods and the libraries I created that the mods use.

## Build on your own machine

1. Ensure you have the dotnet 4.7.2 devpack installed.
   * Either [Download the installer](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer) from Microsoft
   * If you are using [chocolatey](https://chocolatey.org/) you can install it by running `choco install netfx-4.7.2-devpack`
2. Set an Environmental Variable called `CS_GAME_DIRECTORY` which points to the Cities_Skylines game install folder. E.g. `C:\games\steamapps\common\Cities_Skylines`

This variable is used in the `.csproj` files to locate the dll files provided by the game

## Testing the mods in game

When you build the solution the mods are automatically deployed to the `%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods` folder.
