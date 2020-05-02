using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{
    private Tilemap map;

    public int worldWidth = 10;
    public int worldHeight = 10;

    public TileBase dirt;
    public TileBase stone;

    public static Planet.PlanetData data;

    public float noiseScale = 1;
    public int seed = 1;
    public Vector2 offset = new Vector2(0, 0);

    public float oreScale = 0.1f;

    public float[] oreThresholds = new float[1];

    public float range = 1;

    System.Random rng;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();

        rng = new System.Random(seed);

        generateTopLevelTerrain();
        generateStonePatches();
    }

    void generateTopLevelTerrain()
    {
        float[] noiseMap = Noise.GenerateNoiseMap(worldWidth, seed, noiseScale + 0.0001f, offset);
        for (int i = 0; i < worldWidth; i++)
        {
            noiseMap[i] = noiseMap[i] * (Mathf.Abs(range) * 2) - (Mathf.Abs(range));
        }

        for (int i = -(worldWidth / 2); i < worldWidth / 2; i++)
        {
            for (int j = -worldHeight; j < noiseMap[i + (worldWidth / 2)]; j++)
            {
                map.SetTile(new Vector3Int(i, j, 0), dirt);
            }
        }
    }

    void generateStonePatches()
    {
        float offsetX = rng.Next(-100, 101) + 1000;
        float offsetY = rng.Next(-100, 101) + 1000;

        for (int i = -(worldWidth / 2); i < worldWidth / 2; i++)
        {
            for (int j = -worldHeight; j < 0; j++)
            {
                float weight = Mathf.PerlinNoise((i + offsetX) * oreScale, (j + offsetY) * oreScale);

				weight *= Mathf.Clamp((-j) * 0.05f, 0, 9);

                if (weight > oreThresholds[0])
                {
                  	if (map.GetTile(new Vector3Int(i, j, 0)) != null)
                  	{
                    	map.SetTile(new Vector3Int(i, j, 0), stone);
                  	}
                }
            }
        }
    }
}
