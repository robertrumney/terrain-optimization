using UnityEngine;
using System.Collections.Generic;

// This script adjusts the detail settings of terrains in the scene based on the player's position
public class AdjustTerrainDetail : MonoBehaviour
{
    #region Configuration
    // Minimum values for detailObjectDistance and heightmapPixelError
    public float minDetailObjectDistance = 30;
    public float minHeightmapPixelError = 3;

    // List to store the default values for detailObjectDistance and heightmapPixelError for each terrain
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

    // This function updates the terrain detail settings, applying an edge check to prevent the player from getting too close to the edges of the terrain
    private void Update()
    {
        // Loop through all the terrains in the scene
        foreach (Terrain terrain in terrains)
        {
            // Calculate the distance from the player to the terrain
            float distance = Vector3.Distance(playerTransform.position, terrain.transform.position);

            // Calculate the edge buffer distance, which determines how close the player can get to the edges of the terrain
            float edgeBuffer = 10;

            // Check if the player's position is within the allowed range
            if
                (
                    playerTransform.position.x < terrain.transform.position.x + terrain.terrainData.size.x / 2 - edgeBuffer &&
                    playerTransform.position.x > terrain.transform.position.x - terrain.terrainData.size.x / 2 + edgeBuffer &&
                    playerTransform.position.z < terrain.transform.position.z + terrain.terrainData.size.z / 2 - edgeBuffer &&
                    playerTransform.position.z > terrain.transform.position.z - terrain.terrainData.size.z / 2 + edgeBuffer
                )
            {
                // Player is within the edge buffer, so don't adjust the terrain detail settings
                // Get the default values for the current terrain
                (float defaultDetailObjectDistance, float defaultHeightmapPixelError) = defaultValues[terrains.IndexOf(terrain)];

                // Set the terrain's detailObjectDistance and heightmapPixelError properties to their default values
                terrain.detailObjectDistance = defaultDetailObjectDistance;
                terrain.heightmapPixelError = defaultHeightmapPixelError;
            }
            else
            {
                // Update the terrain's detail settings based on the player's position
                terrain.detailObjectDistance = Mathf.Max(distance * 0.5f, minDetailObjectDistance);
                terrain.heightmapPixelError = Mathf.Max(distance * 0.1f, minHeightmapPixelError);
            }
        }
    }
    #endregion
}
