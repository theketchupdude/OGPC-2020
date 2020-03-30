using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGPC;

public class Planet : MonoBehaviour
{
    double temperature = 0.0; // in Celsius because metric

    public Item dirtItem;
    public Item stoneItem;

    public Item resourceCommon;
    public Item resourceUncommon;
    public Item resourceRare;

    public void RandomizeStats(int seed)
    {
        System.Random rng = new System.Random(seed);

        temperature = (NextGaussianDouble() * 60);

        print(temperature);
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
}
