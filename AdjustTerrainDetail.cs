using UnityEngine;
using System.Collections.Generic;

public class AdjustTerrainDetail : MonoBehaviour
{
    #region Configuration
    // Minimum values for detailObjectDistance and heightmapPixelError
    public float minDetailObjectDistance = 30;
    public float minHeightmapPixelError = 3;
    #endregion

    #region Object References
    [System.NonSerialized]
    List<Terrain> terrains;

    [System.NonSerialized]
    Transform playerTransform;
    #endregion

    #region MonoBehaviours
    private void Awake()
    {
        this.enabled = false;
    }

    private void Start()
    {
        // Find all terrains in the scene
        terrains = new List<Terrain>(FindObjectsOfType<Terrain>());

        // Find the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Adjust terrain detail settings based on the player's position
        foreach (Terrain terrain in terrains)
        {
            // Calculate the distance from the player to the terrain
            float distance = Vector3.Distance(playerTransform.position, terrain.transform.position);

            // Check if the player is within a certain distance of the edges of the terrain
            float edgeBuffer = 10;  // Adjust this value to control how close the player can get to the edges
            if (playerTransform.position.x < terrain.transform.position.x + terrain.terrainData.size.x / 2 - edgeBuffer ||
                playerTransform.position.x > terrain.transform.position.x - terrain.terrainData.size.x / 2 + edgeBuffer ||
                playerTransform.position.z < terrain.transform.position.z + terrain.terrainData.size.z / 2 - edgeBuffer ||
                playerTransform.position.z > terrain.transform.position.z - terrain.terrainData.size.z / 2 + edgeBuffer)
            {
                // Player is within the edge buffer, so don't adjust the terrain detail settings
                continue;
            }

            // Update the terrain's detail settings based on the player's position
            terrain.detailObjectDistance = Mathf.Max(distance * 0.5f, minDetailObjectDistance);
            terrain.heightmapPixelError = Mathf.Max(distance * 0.1f, minHeightmapPixelError);
        }
    }
    #endregion
}
