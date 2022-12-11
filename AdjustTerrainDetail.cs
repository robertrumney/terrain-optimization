using UnityEngine;
using System.Collections.Generic;

public class AdjustTerrainDetail : MonoBehaviour
{
    #region Configuration
    // Minimum values for detailObjectDistance and heightmapPixelError
    public float minDetailObjectDistance = 30;
    public float minHeightmapPixelError = 3;
    #endregion

    #region OBJECT REFERENCES
    [System.NonSerialized]
    List<Terrain> terrains;

    [System.NonSerialized]
    Transform playerTransform;
    #endregion

    #region MONOBEHAVIOURS
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

            // Update the terrain's detail settings based on the player's position
            terrain.detailObjectDistance = Mathf.Max(distance * 0.5f, minDetailObjectDistance);
            terrain.heightmapPixelError = Mathf.Max(distance * 0.1f, minHeightmapPixelError);
        }
    }
    #endregion
}
