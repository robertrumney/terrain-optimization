using UnityEngine;

public class TerrainOptimizationVolume : MonoBehaviour
{
    // Array of TerrainSetting objects that contain a reference to a Terrain object
    // and a distance value used to optimize rendering of trees within the terrain
    public TerrainSetting[] terrains;

    // Nested class used to define TerrainSetting objects with public properties
    [System.Serializable]
    public class TerrainSetting
    {
        public Terrain targetTerrain; // Reference to a Terrain object
        public float billboardDistance; // Distance value used to optimize rendering of trees within the terrain
    }

    // Private method used to set the treeBillboardDistance property of each terrain object in the terrains array
    // based on the value of the boolean parameter passed to the method
    private void SetTerrains(bool x)
    {
        if (x)
        {
            // Iterate through the terrains array and set the treeBillboardDistance property of each terrain object
            // to the corresponding billboardDistance value in its TerrainSetting object
            foreach (TerrainSetting ts in terrains)
            {
                ts.targetTerrain.treeBillboardDistance = ts.billboardDistance;
            }
        }
        else
        {
            // Iterate through the terrains array and set the treeBillboardDistance property of each terrain object
            // to a default value of 1000
            foreach (TerrainSetting ts in terrains)
            {
                ts.targetTerrain.treeBillboardDistance = 1000;
            }
        }
    }

    // OnTriggerEnter is called when the player enters the trigger area associated with this script's GameObject
    private void OnTriggerEnter(Collider x)
    {
        // Check if the collider's tag is "Player"
        if (x.CompareTag("Player"))
        {
            // If the collider's tag is "Player", call the SetTerrains method with a value of "true"
            // to optimize the rendering of trees within the terrain
            SetTerrains(true);
        }
    }

    // OnTriggerExit is called when the player exits the trigger area associated with this script's GameObject
    private void OnTriggerExit(Collider x)
    {
        // Check if the collider's tag is "Player"
        if (x.CompareTag("Player"))
        {
            // If the collider's tag is "Player", call the SetTerrains method with a value of "false"
            // to reset the treeBillboardDistance property of each terrain object to a default value
            SetTerrains(false);
        }
    }
}
