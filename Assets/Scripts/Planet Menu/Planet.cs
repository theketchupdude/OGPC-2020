using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGPC;

public class Planet : MonoBehaviour
{
    double temperature = 0.0; // in Celsius because metric

    double planetFoliageHue;

    public bool isIcePlanet;
    public bool isDesertPlanet;

    public Item snowItem;

    public Item dirtItem;
    public Item sandItem;
    public Item stoneItem;

    public Item resourceCommon;
    public Item resourceUncommon;
    public Item resourceRare;

    public Planet[] adjacents;

    void Start()
    {
        RandomizeStats(Random.Range(int.MinValue, int.MaxValue));

        GetComponent<CircleCollider2D>().enabled = false;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 20);

        adjacents = new Planet[cols.Length];

        for (int i = 0; i < cols.Length; i++) {
            adjacents[i] = cols[i].gameObject.GetComponent<Planet>();
        }

        GetComponent<CircleCollider2D>().enabled = true;
    }

    void RandomizeStats(int seed)
    {
        System.Random rng = new System.Random(seed);

        temperature = (NextGaussianDouble() * 40) + 20;

        isIcePlanet = temperature < 0.0;
        isDesertPlanet = temperature > 40.0;

        planetFoliageHue = Random.Range(0, 360);

        GetComponent<SpriteRenderer>().material.SetFloat("_Hue", (float)planetFoliageHue);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < adjacents.Length; i++) {
            Gizmos.DrawLine(transform.position, adjacents[i].gameObject.transform.position);
        }
    }
}
