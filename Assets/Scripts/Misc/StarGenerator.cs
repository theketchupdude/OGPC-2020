using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StarGenerator : MonoBehaviour
{
    public int numberOfStars = 100;

    public Star[] stars;

    private GameObject[] objs;

    void Start()
    {
        for(int i = 0; i < numberOfStars; i++)
        {
            objs[i] = new GameObject();
            objs[i].transform.parent = gameObject.transform;
            objs[i].AddComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        foreach (Star s in stars)
        {
            s.nextFrame();
        }
    }

    [Serializable]
    public class Star
    {
        public Sprite[] anim;

        public Vector3 pos;

        private int frame = 0;

        public void nextFrame()
        {
            frame = (frame + 1) % anim.Length;
        }

        public Sprite getFrame()
        {
            return anim[frame];
        }
    }
}
