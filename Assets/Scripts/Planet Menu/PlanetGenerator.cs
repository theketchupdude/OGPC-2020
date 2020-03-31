using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGPC;

public class PlanetGenerator : MonoBehaviour
{
    public int numberOfPlanets = 25;

    public int spawnTriesPerPlanet = 9;

    public double planetSpawnAreaSize = 30.0;

    public GameObject planetPrefab;

    public CameraPan panCamera;

    public Item[] dirtItems;
    public Item[] stoneItems;

    void Start()
    {
        SpawnPlanet(new Vector3(0, 0, 0), "Ship");

        panCamera.currentPlanet = SpawnPlanet(new Vector3(0, (float)planetSpawnAreaSize, 0), "Start");


        for (int i = 2; i < numberOfPlanets; i++)
        {
            SpawnPlanet(FindSpawnLocation(spawnTriesPerPlanet), "Planet #" + i);
        }
    }

    private Vector3 FindSpawnLocation(int maxAttempts)
    {
        Vector3 pos = Vector3.zero;

        int attempts = 0;

        bool valid = false;

        while (!valid && attempts < maxAttempts)
        {
            attempts++;

            pos = Random.insideUnitCircle * (float)planetSpawnAreaSize;

            valid = Physics2D.OverlapCircle(pos, (float)8) == null;
        }

        return pos;
    }

    private GameObject SpawnPlanet(Vector3 loc, string name)
    {
        GameObject planet = Instantiate(planetPrefab);

        planet.name = name;

        planet.transform.parent = gameObject.transform;

        planet.transform.position = loc;

        Planet planetComponent = planet.GetComponent<Planet>();

        planetComponent.dirtItem = dirtItems[Random.Range(0, dirtItems.Length)];
        planetComponent.stoneItem = stoneItems[Random.Range(0, stoneItems.Length)];

        return planet;
    }
}
