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
    
    void Start()
    {
        map = gameObject.GetComponent<Tilemap>();

        generateTopLevelTerrain(1);
    }

    void generateTopLevelTerrain(int seed)
    {
        for (int i = -(worldWidth / 2); i < worldWidth / 2; i++)
        {
            for (int j = -worldHeight; j < 0; j++)
            {
                map.SetTile(new Vector3Int(i, j, 0), dirt);
            }
        }
    }
}
