using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{
    private Tilemap map;

    public int worldWidth = 10;
    public int worldHeight = 10;

    public Planet.PlanetData data;

    public float noiseScale = 1;
    public int seed = 1;
    public Vector2 offset = new Vector2(0, 0);

    public float oreScale = 0.1f;

    public float[] oreThresholds = new float[4];

    public float range = 1;

    System.Random rng;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();

        rng = new System.Random(seed);

        GenerateTopLevelTerrain();
        GenerateStonePatches();
        GenerateOre(oreThresholds[1], data.commonOre);
        GenerateOre(oreThresholds[2], data.mediumOre);
        GenerateOre(oreThresholds[3], data.rareOre);
    }

    void GenerateTopLevelTerrain()
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
                map.SetTile(new Vector3Int(i, j, 0), data.dirtTile);
            }
        }
    }

    void GenerateStonePatches()
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
                    	map.SetTile(new Vector3Int(i, j, 0), data.stoneTile);
                  	}
                }
            }
        }
    }

    void GenerateOre(float threshold, TileBase ore)
    {
        float offsetX = rng.Next(-100, 101) + 1000;
        float offsetY = rng.Next(-100, 101) + 1000;

        for (int i = -(worldWidth / 2); i < worldWidth / 2; i++)
        {
            for (int j = -worldHeight; j < 0; j++)
            {
                float weight = Mathf.PerlinNoise((i + offsetX) * oreScale, (j + offsetY) * oreScale);

                if (weight > threshold)
                {
                  	if (map.GetTile(new Vector3Int(i, j, 0)) != null)
                  	{
                    	map.SetTile(new Vector3Int(i, j, 0), ore);
                  	}
                }
            }
        }
    }
}
