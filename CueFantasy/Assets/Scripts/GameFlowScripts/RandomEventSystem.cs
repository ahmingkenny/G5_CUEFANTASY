using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventSystem : MonoBehaviour
{
    private int chanceDigit;
    private int locationDigit;
    private TerrainEventBehaviour terrainEventBehaviour;
    void Start()
    {

    }

    public void CreateEvent()
    {
        GameObject[] Terrain = GameObject.FindGameObjectsWithTag("Terrain");
        chanceDigit = Random.Range(0, 100);
        locationDigit = Random.Range(0, Terrain.Length);
        GameObject terrain = Terrain[locationDigit];
        if (chanceDigit < 5 && !terrain.GetComponent<TerrainEventBehaviour>().isEventing)
        {
            terrain.GetComponent<TerrainEventBehaviour>().CreateRainStorm();
        }
        else if (chanceDigit >= 5 && chanceDigit < 10 && !terrain.GetComponent<TerrainEventBehaviour>().isEventing)
        {
            terrain.GetComponent<TerrainEventBehaviour>().CreateSnowStorm();
        }
        else if (chanceDigit >= 10 && chanceDigit < 15 && !terrain.GetComponent<TerrainEventBehaviour>().isEventing)
        {
            terrain.GetComponent<TerrainEventBehaviour>().CreateThirdPartyInvasion();
        }
    }

}
