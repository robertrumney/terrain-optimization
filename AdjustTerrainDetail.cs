//Add this code to your camera to automatically increase FPS by adjusting terrain detail and quality based on player world position
using UnityEngine;
using System.Collections.Generic;

public class AdjustTerrainDetail : MonoBehaviour
{
    [System.NonSerialized]
    List<Terrain> terrains;

    [System.NonSerialized]
    Transform playerTransform;

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
            terrain.detailObjectDistance = distance * 0.5f;
            terrain.heightmapPixelError = distance * 0.1f;
        }
    }
}
