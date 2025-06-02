# OmegaPlug

OmegaPlug is a .NET 6 plugin designed for use with BepInEx and [_My Dystopian Robot Girlfriend_](https://incontinentcell.itch.io/factorial-omega) It integrates with the library Buttplug and Harmony to extend game functionality to ButPlugIO toys.

## Features

- Integrates with [Buttplug](https://buttplug.io/) for device control, at this moment depth is strength of viberations toys.

## Requirements
- .NET 6 runtime

## Installation

1. **Install BepInEx 6 (IL2CPP)** 
   - [Download](https://builds.bepinex.dev/projects/bepinex_be/735/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.735%2B5fef357.zip) and extract BepInEx 6 to your game's root directory. BepInEx-`BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.735%2B5fef357.zip` is what I used to make this.
   - Run the game once to generate the `BepInEx` folder structure.
   - Close the game.

2. **Unzip OmegaPlug**
   - Download the compiled zip
   - Place `BepInEx` into your game's main directory, replacing `Newtonsoft.Json.dll`.

4. **Configure (Optional)**
   - Edit any configuration files as needed in `BepInEx/config`. (none so far, place holder)

## Usage

1. Launch IntifaceCentral or your preferred Buttplug server.
   - Ensure your Buttplug server is running and devices are connected.
2. Launch the game.
   - OmegaPlug will automatically connect to the Buttplug server and manage device interactions.

## Development
- Target framework: `.NET 6`

### Libraries Folder
This project uses a `libs` folder to manage dependencies. The following libraries are required and not provided you must find them your self from your game files.:
- 0Harmony.dll
- GameAssembly.dll
- Il2Cppmscorlib.dll
- UnityEngine.CoreModule.dll
- UnityEngine.dll
Included are:
- Buttplug.dll (built from the Buttplug C# library on 6.0)
- Updated Newtonsoft.Json.dll (To be replaced in interop folder for butplug to work.)

## Troubleshooting

- Ensure all required DLLs are present in the correct directories.
- Check the BepInEx console/logs for errors.
- Make sure you are using the correct version of BepInEx for your game (IL2CPP).

## License

This project is provided as-is. See individual library licenses for third-party dependencies.

---

**Note:** This README assumes you are familiar with BepInEx and Unity modding basics. For more information, visit the [BepInEx documentation](https://docs.bepinex.dev/).
