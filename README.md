# AdjustTerrainDetail

The `AdjustTerrainDetail` script is a simple utility for Unity that automatically adjusts the terrain detail and quality settings based on the player's position in the world. This can help to improve the performance and visual quality of your game by increasing the level of detail and quality of the terrain in areas that are near the player, and reducing the level of detail and quality in areas that are far from the player.

# TerrainOptimizationVolume

The `TerrainOptimizationVolume` script is an optional module for Unity that optimizes terrain rendering by adjusting the distance at which trees are rendered based on the player's position within trigger areas. It includes a public array of "TerrainSetting" objects, each defining a reference to a Terrain object and a distance value for optimizing tree rendering. The script has two trigger methods that call a private method to set the "treeBillboardDistance" property of each terrain object in the array.

## Usage

To use the `AdjustTerrainDetail` script, simply add it to the camera in your scene. The script will automatically find all terrains in the scene and update their detail and quality settings based on the player's position.

## License

The `AdjustTerrainDetail` script is licensed under the MIT license. You are free to use, modify, and distribute the script as you see fit, subject to the terms of the license.

## Copyright

The `AdjustTerrainDetail` script is copyright © 2022 Robert Rumney. You are free to use the script without the need for attribution.

## Contact

If you have any questions, suggestions, or feedback about the `AdjustTerrainDetail` script, please don't hesitate to contact me. You can reach me at [robertrumney@gmail.com](mailto:robertrumney@gmail.com) or on [Twitter](https://twitter.com/rumnizzle). I would love to hear from you and see how you are using the script in your projects!
