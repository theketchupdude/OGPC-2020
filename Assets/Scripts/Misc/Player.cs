using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public int speed = 1;

    private bool onGround = true;

    private Camera mainCamera;

    private Tilemap map;

    public GameObject tilemap;

    public GameObject destroySprite;
    public Sprite[] destroySprites;

    private float destroyTimer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        map = tilemap.GetComponent<Tilemap>();
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetAxis("Jump") > 0.5 && onGround)
        {
            move.y += 12;
            onGround = false;
        }

        rb.velocity = move;

        //Mining/destroying blocks
        if (Input.GetMouseButton(0)) 
        {
            Vector3 wP = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePos = new Vector3Int((int)Math.Floor(wP.x), (int)Math.Floor(wP.y), 0);

            destroyTimer += Time.deltaTime * 4;

            if (destroyTimer > 9)
            {
                map.SetTile(tilePos, null);
                destroyTimer = 0;
            }
            else 
            {
                destroySprite.GetComponent<SpriteRenderer>().sprite = destroySprites[(int)Math.Floor(destroyTimer)];
                destroySprite.transform.position = new Vector3(tilePos.x + 0.5f, tilePos.y + 0.5f, -0.5f);
            }
        }
        else 
        {
            destroyTimer = 0f;
            destroySprite.GetComponent<SpriteRenderer>().sprite = destroySprites[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Vector2.Angle(other.GetContact(0).normal, Vector2.up) < 45)
        {
            onGround = true;
        }
    }
}
