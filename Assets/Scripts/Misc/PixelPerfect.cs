using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    public int PPU = 16;
    public float PPUScale = 1;

    void Update()
    {
        gameObject.GetComponent<Camera>().orthographicSize = (Screen.height / (PPUScale * PPU)) * 0.5f;
    }
}
