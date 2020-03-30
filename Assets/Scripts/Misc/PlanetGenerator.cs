using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public int numberOfPlanets = 25;

    public int spawnTriesPerPlanet = 9;

    public double planetSpawnAreaSize = 30.0;

    public GameObject planetPrefab;

    void Start()
    {
        SpawnPlanet(new Vector3(0, 0, 0), "Ship");

        SpawnPlanet(new Vector3(0, (float)planetSpawnAreaSize, 0), "Start");


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

    private void SpawnPlanet(Vector3 loc, string name)
    {
        GameObject planet = Instantiate(planetPrefab);

        planet.name = name;

        planet.GetComponent<Planet>().RandomizeStats(Random.Range(int.MinValue, int.MaxValue));

        planet.transform.parent = gameObject.transform;

        planet.transform.position = loc;
    }
}
