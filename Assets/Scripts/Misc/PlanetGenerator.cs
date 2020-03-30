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
        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 location = FindSpawnLocation(spawnTriesPerPlanet);

            GameObject planet = Instantiate(planetPrefab);

            planet.name = "Planet #" + i;

            planet.GetComponent<Planet>().RandomizeStats(Random.Range(int.MinValue, int.MaxValue));

            planet.transform.parent = gameObject.transform;

            planet.transform.position = location;
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
}
