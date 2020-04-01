using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLine : MonoBehaviour
{
    Planet source;
    Planet dest;

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0, source.gameObject.transform.position);
        line.SetPosition(1, dest.gameObject.transform.position);
    }
}
