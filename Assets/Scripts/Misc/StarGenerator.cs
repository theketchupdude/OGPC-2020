using UnityEngine;
using System;

public class StarGenerator : MonoBehaviour
{
    public int numberOfStars = 100;

    public Star[] stars;

    private AnimatedStar[] objs;

    void Start()
    {
        objs = new AnimatedStar[numberOfStars];

        for(int i = 0; i < numberOfStars; i++)
        {
            GameObject obj = new GameObject("Star");
            obj.transform.parent = gameObject.transform;
            obj.transform.position = new Vector3(UnityEngine.Random.Range(-30, 30), UnityEngine.Random.Range(-30, 30), 1);
            objs[i] = obj.AddComponent<AnimatedStar>();
            objs[i].star = stars[UnityEngine.Random.Range(0, stars.Length)];
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime);
    }

    [Serializable]
    public class Star
    {
        public Sprite[] anim;

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

    public class AnimatedStar : MonoBehaviour
    {
        public Star star;

        private SpriteRenderer sprite;

        void Awake()
        {
            sprite = gameObject.AddComponent<SpriteRenderer>();
        }

        void Update()
        {
            sprite.sprite = star.getFrame();
        }
    }
}
