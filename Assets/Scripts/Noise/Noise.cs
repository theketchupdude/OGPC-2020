using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[] GenerateNoiseMap(int mapWidth, int seed, float scale, Vector2 offset)
    {
        float[] noiseMap = new float[mapWidth];

        System.Random prng = new System.Random(seed);

        float amplitude = 1;
        float frequency = 1;

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;

        for (int x = 0; x < mapWidth; x++)
        {
            amplitude = 1;
            frequency = 1;
            float noiseHeight = 0;

            float sampleX = (x - halfWidth + prng.Next(-100000, 100000)) / scale * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, offset.y) * 2 - 1;
            noiseHeight += perlinValue * amplitude;

            if (noiseHeight > maxLocalNoiseHeight)
            {
                maxLocalNoiseHeight = noiseHeight;
            }
            else if (noiseHeight < minLocalNoiseHeight)
            {
                minLocalNoiseHeight = noiseHeight;
            }

            noiseMap[x] = noiseHeight;
        }


        for (int x = 0; x < mapWidth; x++)
        {
            noiseMap[x] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x]);
        }

        return noiseMap;
    }
}
