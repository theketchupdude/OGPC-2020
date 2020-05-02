using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using OGPC;

public class Planet : MonoBehaviour
{
    double temperature = 0.0; // in Celsius because metric

    double planetFoliageHue;

    public bool isIcePlanet;
    public bool isDesertPlanet;

    public PlanetData data;

    public GameObject linePrefab;

    public bool selected;

    GameObject[] adjacents;

    float connectRadius = 20;

    public int number;

    void Awake()
    {
        RandomizeStats(Random.Range(int.MinValue, int.MaxValue));
    }

    public void InitAdjacents()
    {
        GetComponent<CircleCollider2D>().enabled = false;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, connectRadius);

        adjacents = new GameObject[cols.Length];

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.GetComponent<Planet>() != null)
            {
                adjacents[i] = cols[i].gameObject;
                
                if (adjacents[i].GetComponent<Planet>().number < number)
                {
                    Instantiate(linePrefab, transform).GetComponent<PlanetLine>().SetPlanets(this, adjacents[i].GetComponent<Planet>());
                }
            }
        }

        GetComponent<CircleCollider2D>().enabled = true;
    }

    void RandomizeStats(int seed)
    {
        System.Random rng = new System.Random(seed);

        temperature = (NextGaussianDouble() * 40) + 20;

        isIcePlanet = temperature < 0.0;
        isDesertPlanet = temperature > 40.0;

        if (isIcePlanet)
        {
            planetFoliageHue = 120 + (NextGaussianDouble() * 35);
        }
        else if (isDesertPlanet)
        {
            planetFoliageHue = 290 + (NextGaussianDouble() * 35);
        }
        else
        {
            planetFoliageHue = 170 + (NextGaussianDouble() * 50);
        }

        GetComponent<Image>().material.SetFloat("_Hue", (float)planetFoliageHue);
    }

    private static double NextGaussianDouble()
    {
        double u, v, S;

        do
        {
            u = 2.0 * Random.value - 1.0;
            v = 2.0 * Random.value - 1.0;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        double fac = Mathf.Sqrt(-2.0f * Mathf.Log((float)S) / (float)S);
        return u * fac;
    }

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position , transform.forward, connectRadius);
    }

    void OnMouseDown()
    {
        Camera.main.GetComponent<CameraPan>().SetPlanet(gameObject);
        GenerateTilemap.data = this.data;
        //SceneManager.LoadScene("LoadingScreen");
    }

    public struct PlanetData
    {
        public PlanetData(TileBase dirt, TileBase sand, TileBase stone, TileBase rare, TileBase medium, TileBase common)
        {
            dirtTile = dirt;
            sandTile = sand;
            stoneTile = stone;

            rareOre = rare;
            mediumOre = medium;
            commonOre = common;
        }

        TileBase dirtTile;
        TileBase sandTile;
        TileBase stoneTile;

        TileBase rareOre;
        TileBase mediumOre;
        TileBase commonOre;
    }
}
