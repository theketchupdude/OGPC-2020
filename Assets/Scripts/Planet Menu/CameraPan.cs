using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    Camera cam;

    public Vector2 mouseBorder = new Vector2();

    bool controlEnabled = false;

    GameObject currentPlanet;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 movement = new Vector3();

        if (controlEnabled) {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (mousePosition.y > cam.orthographicSize - mouseBorder.y) {
                movement.y += mousePosition.y - (cam.orthographicSize - mouseBorder.y);
            }
            if (mousePosition.y < -cam.orthographicSize + mouseBorder.y) {
                movement.y += mousePosition.y - (-cam.orthographicSize + mouseBorder.y);
            }

            float xSize = cam.orthographicSize * cam.aspect;

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

    void SmoothPan(Vector3 origin, Vector3 pan)
    {
        origin.z = transform.position.z;

        pan.z = 0;

        transform.position = Vector3.Lerp(transform.position, origin + pan, Time.deltaTime);
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
