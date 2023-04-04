using UnityEngine;
using System.Collections.Generic;

public class AdjustTerrainDetail : MonoBehaviour
{
    #region Configuration
    // Minimum values for detailObjectDistance and heightmapPixelError
    public float minDetailObjectDistance = 30;
    public float minHeightmapPixelError = 3;

    // List of tuples to store the default values for detailObjectDistance and heightmapPixelError for each terrain
    private readonly List<(float, float)> defaultValues = new List<(float, float)>();
    #endregion

    #region Object References
    // List to store references to all the terrains in the scene
    [System.NonSerialized]
    List<Terrain> terrains;

    // Reference to the player's transform
    [System.NonSerialized]
    Transform playerTransform;
    #endregion

    #region MonoBehaviours
    // This function is called when the script is first enabled
    private void Start()
    {
        // Find all terrains in the scene
        terrains = new List<Terrain>(FindObjectsOfType<Terrain>());

        // Find the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Store the default values for detailObjectDistance and heightmapPixelError for each terrain
        foreach (Terrain terrain in terrains)
        {
            defaultValues.Add((terrain.detailObjectDistance, terrain.heightmapPixelError));
        }
    }

    // This function calculates and displays the distance between the player and the nearest edge of the terrain they are on
    private void Update()
    {
        // Find the terrain that the player is on
        Terrain terrain = FindTerrain(playerTransform.position);

        // Calculate the distance from the player to the nearest edge of the terrain
        float distanceToEdge = CalculateDistanceToEdge(playerTransform.position, terrain);

        // Update the terrain detail settings based on the player's position
        terrain.detailObjectDistance = Mathf.Max(distanceToEdge * 0.5f, minDetailObjectDistance);
        terrain.heightmapPixelError = Mathf.Max(distanceToEdge * 0.1f, minHeightmapPixelError);
    }

    // This function is called when the script is destroyed
    private void OnDestroy()
    {
        // Loop through all the terrains in the scene
        for (int i = 0; i < terrains.Count; i++)
        {
            // Reset the detailObjectDistance and heightmapPixelError values for the terrain to their default values
            terrains[i].detailObjectDistance = defaultValues[i].Item1;
            terrains[i].heightmapPixelError = defaultValues[i].Item2;
        }
    }
    #endregion

    #region Custom Functions
    // This function finds the terrain that the specified position is on
    private Terrain FindTerrain(Vector3 position)
    {
        // Loop through all the terrains in the scene
        foreach (Terrain terrain in terrains)
        {
            Vector3 center = terrain.transform.position + new Vector3(terrain.terrainData.size.x / 2, 0, terrain.terrainData.size.z / 2);

            // Check if the position is within the bounds of the terrain
            if (position.x < center.x + terrain.terrainData.size.x / 2 &&
                position.x > center.x - terrain.terrainData.size.x / 2 &&
                position.y < center.y + terrain.terrainData.size.y / 2 &&
                position.y > center.y - terrain.terrainData.size.y / 2 &&
                position.z < center.z + terrain.terrainData.size.z / 2 &&
                position.z > center.z - terrain.terrainData.size.z / 2)
            {
                // Position is within the bounds of the terrain, so return a reference to the terrain
                return terrain;
            }
        }

        // No terrain was found at the specified position, so return null
        return null;
    }

    // This function calculates the distance between the specified position and the nearest edge of the specified terrain
    private float CalculateDistanceToEdge(Vector3 position, Terrain terrain)
    {
        // Calculate the distance to the nearest edge along the x axis
        float distanceToEdgeX = Mathf.Min(
            position.x - (terrain.transform.position.x - terrain.terrainData.size.x / 2),
            (terrain.transform.position.x + terrain.terrainData.size.x / 2) - position.x
        );

        // Calculate the distance to the nearest edge along the z axis
        float distanceToEdgeZ = Mathf.Min(
            position.z - (terrain.transform.position.z - terrain.terrainData.size.z / 2),
            (terrain.transform.position.z + terrain.terrainData.size.z / 2) - position.z
        );

        // Return the minimum distance to the nearest edge along either axis
        return Mathf.Min(distanceToEdgeX, distanceToEdgeZ);
    }
    #endregion
}
