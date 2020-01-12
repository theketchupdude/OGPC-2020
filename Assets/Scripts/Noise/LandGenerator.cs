using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour
{
    public float noiseScale;

    [Tooltip("Amount of points you want to calculate")]
    public int worldSize = 10;

    public int seed;

    public Vector2 offset;

    public float range = 1; // max and min value (must be positive)

    public AnimationCurve outputCurve; // only used for visualization in debug

    private void Start()
    {
        float[] noiseMap = Noise.GenerateNoiseMap(worldSize, seed, noiseScale + 0.0001f, offset);
        for (int i = 0; i < worldSize; i++)
        {
            noiseMap[i] = noiseMap[i] * (Mathf.Abs(range) * 2) - (Mathf.Abs(range));
            outputCurve.AddKey(i, noiseMap[i]);
        }
    }
}
