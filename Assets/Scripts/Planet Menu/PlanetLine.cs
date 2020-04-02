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
        Vector3 sourcePos = source.gameObject.transform.position;
        sourcePos.z = transform.position.z;

        Vector3 destPos = dest.gameObject.transform.position;
        destPos.z = transform.position.z;

        line.SetPosition(0, sourcePos);
        line.SetPosition(1, destPos);

        if (source.selected || dest.selected)
        {
            line.endColor = Color.yellow;
            line.startColor = Color.yellow;
        }
        else
        {
            line.endColor = Color.white;
            line.startColor = Color.white;
        }
    }

    public void SetPlanets(Planet first, Planet second)
    {
        source = first;
        dest = second;
    }
}
