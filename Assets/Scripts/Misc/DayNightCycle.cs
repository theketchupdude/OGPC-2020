using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public GameObject follow;

    public GameObject lightObj;

    public GameObject sun;
    public GameObject moon;

    public float speedMultiplier = 1;
    public float lightMultiplier = 1;

    public float time = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = ((Time.deltaTime * speedMultiplier) + time) % 360;

        //lightObj.GetComponent<Light>().intensity = (time / 360) * lightMultiplier;

        if (time < 180)
        {
            sun.SetActive(true);
            moon.SetActive(false);

            float sunX = (((time % 180) - 90) / 90) * 31;
            float sunY = -0.01f * (sunX - 36) * (sunX + 36) + 5;

            sun.transform.position = new Vector3(sunX, sunY, 1) + follow.transform.position;
            sun.transform.right = follow.transform.position - sun.transform.position;
        }
        else 
        {
            sun.SetActive(false);
            moon.SetActive(true);

            float moonX = (((time % 180) - 90) / 90) * 31;
            float moonY = -0.01f * (moonX - 36) * (moonX + 36) + 5;

            moon.transform.position = new Vector3(moonX, moonY, 1) + follow.transform.position;
            moon.transform.right = follow.transform.position - moon.transform.position;
        }
    }
}
