using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{
    private Tilemap map;

    public int worldWidth = 10;
    public int worldHeight = 10;

    public Tile dirt;

    public float noiseScale = 1;
    public int seed = 1;
    public Vector2 offset = new Vector2(0, 0);

    public float range = 1;

    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();

        generateTopLevelTerrain();
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
}
