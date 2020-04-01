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
        SpawnPlanet(new Vector3(0, 0, 0), 0);

        GameObject startPlanet = SpawnPlanet(new Vector3(0, (float)planetSpawnAreaSize, 0), 1);

        for (int i = 2; i < numberOfPlanets; i++)
        {
            SpawnPlanet(FindSpawnLocation(spawnTriesPerPlanet), i);
        }

        panCamera.SetPlanet(startPlanet);
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

    private GameObject SpawnPlanet(Vector3 loc, int num)
    {
        GameObject planet = Instantiate(planetPrefab, transform);

        planet.name = "Planet #" + num;

        planet.transform.position = loc;

        Planet planetComponent = planet.GetComponent<Planet>();

        planetComponent.number = num;

        planetComponent.dirtItem = dirtItems[Random.Range(0, dirtItems.Length)];
        planetComponent.stoneItem = stoneItems[Random.Range(0, stoneItems.Length)];

        return planet;
    }
}
