using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{

    public int numberOfPlanets = 25;

    public GameObject planetPrefab;

    void Start()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            GameObject planet = Instantiate(planetPrefab);

            planet.GetComponent<Planet>().RandomizeStats(Random.Range(int.MinValue, int.MaxValue));
        }
    }
}
