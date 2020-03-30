using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[] GenerateNoiseMap(int mapWidth, int seed, float scale, Vector2 offset)
    {/*
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

        return noiseMap;*/

        float[] noiseMap1 = new float[mapWidth];
        float[] noiseMap2 = new float[mapWidth];
        float[] noiseMap3 = new float[mapWidth];

        float[] noiseMap = new float[mapWidth];

        for (int i = 0; i < mapWidth; i++)
        {
            noiseMap1[i] = Mathf.PerlinNoise((i + seed) * scale, offset.y);
            noiseMap2[i] = Mathf.PerlinNoise((i + seed) * (scale * 2), offset.y) * 0.5f;
            noiseMap3[i] = Mathf.PerlinNoise((i + seed) * (scale * 4), offset.y) * 0.25f;

            noiseMap[i] = (noiseMap1[i] + noiseMap2[i] + noiseMap3[i] + 0.5f + 0.5f) / 5;
        }

        return noiseMap;
    }
}
