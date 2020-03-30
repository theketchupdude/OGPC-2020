using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using OGPC;

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
    public double mineSpeed = 5;

    private double destroyTimer;

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
            wP.z = 0;

            Vector3Int tilePos = new Vector3Int((int)Math.Floor(wP.x), (int)Math.Floor(wP.y), 0);

            DestructibleTile dTile;
            TileBase tile = map.GetTile(tilePos);

            if (tile != null && tile.GetType() == typeof(DestructibleTile))
            {
                dTile = (DestructibleTile)tile;

                destroyTimer += Time.deltaTime * (mineSpeed - dTile.hardness);

                if (destroyTimer > 9)
                {
                    dTile.SpawnDrop(wP);
                    map.SetTile(tilePos, null);
                    destroyTimer = 0;
                }
                else
                {
                    if (destroySprite.transform.position == new Vector3(tilePos.x + 0.5f, tilePos.y + 0.5f, -0.5f))
                    {
                        destroySprite.GetComponent<SpriteRenderer>().sprite = destroySprites[(int)Math.Floor(destroyTimer)];
                    }
                    else
                    {
                        destroySprite.transform.position = new Vector3(tilePos.x + 0.5f, tilePos.y + 0.5f, -0.5f);
                        destroyTimer = 0f;
                        destroySprite.GetComponent<SpriteRenderer>().sprite = destroySprites[0];
                    }
                }
            }
            else
            {
                destroyTimer = 0f;
                destroySprite.GetComponent<SpriteRenderer>().sprite = destroySprites[0];
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
