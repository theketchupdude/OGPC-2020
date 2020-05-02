using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using OGPC;

public class PlanetGenerator : MonoBehaviour
{
    public int numberOfPlanets = 25;

    public int spawnTriesPerPlanet = 9;

    public double planetSpawnAreaSize = 30.0;

    public GameObject planetPrefab;

    public TileBase[] dirtTiles;
    public TileBase[] stoneTiles;
    public TileBase[] oreTiles;

    public Vector2 mouseBorder = new Vector2();

    bool controlEnabled = true;

    GameObject currentPlanet;

    void Start()
    {
        SpawnPlanet(new Vector3(0, 0, 0), 0);

        GameObject startPlanet = SpawnPlanet(new Vector3(0, (float)planetSpawnAreaSize, 0), 1);

        for (int i = 2; i < numberOfPlanets; i++)
        {
            SpawnPlanet(FindSpawnLocation(spawnTriesPerPlanet), i);
        }

        SetPlanet(startPlanet);
    }

    void Update()
    {
        Vector3 movement = new Vector3();

        if (controlEnabled) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (mousePosition.y < Camera.main.orthographicSize - mouseBorder.y) {
                movement.y += mousePosition.y - (-Camera.main.orthographicSize + mouseBorder.y);
            }
            if (mousePosition.y > -Camera.main.orthographicSize + mouseBorder.y) {
                movement.y += mousePosition.y - (Camera.main.orthographicSize - mouseBorder.y);
            }

            float xSize = Camera.main.orthographicSize * Camera.main.aspect;

            if (mousePosition.x > xSize - mouseBorder.x) {
                movement.x += mousePosition.x - (xSize - mouseBorder.x);
            }
            if (mousePosition.x < -xSize + mouseBorder.x) {
                movement.x += mousePosition.x - (-xSize + mouseBorder.x);
            }
        }

        if (currentPlanet != null) {
            SmoothPan(currentPlanet.transform.position, movement * 6);
        }
    }

    private Vector3 FindSpawnLocation(int maxAttempts)
    {
        Vector3 pos = Vector3.zero;

        int attempts = 0;

        bool valid = false;

        while (!valid && attempts < maxAttempts)
        {
            attempts++;

            pos = Random.insideUnitCircle * (float)planetSpawnAreaSize;

            valid = Physics2D.OverlapCircle(pos, (float)8) == null;
        }

        return pos;
    }

    private GameObject SpawnPlanet(Vector3 loc, int num)
    {
        GameObject planet = Instantiate(planetPrefab, transform);

        planet.name = "Planet #" + num;

        planet.transform.position = loc;

        Planet planetComponent = planet.GetComponent<Planet>();

        planetComponent.number = num;

        planetComponent.data = new Planet.PlanetData(
        dirtTiles[Random.Range(0, dirtTiles.Length)],
        dirtTiles[Random.Range(0, dirtTiles.Length)],
        stoneTiles[Random.Range(0, stoneTiles.Length)],
        oreTiles[Random.Range(0, oreTiles.Length)],
        oreTiles[Random.Range(0, oreTiles.Length)],
        oreTiles[Random.Range(0, oreTiles.Length)]
        );

        planetComponent.InitAdjacents();

        return planet;
    }

    void SmoothPan(Vector3 origin, Vector3 pan)
    {
        origin.z = -10;

        pan.z = -10;

        Vector3 pos = Vector3.Lerp(transform.position, pan, Time.deltaTime);
        pos.z = -10;

        transform.position = pos;
    }

    public void SetPlanet(GameObject planet)
    {
        if (currentPlanet != null)
        {
            currentPlanet.GetComponent<Planet>().selected = false;
        }
        planet.GetComponent<Planet>().selected = true;
        currentPlanet = planet;
    }
}
