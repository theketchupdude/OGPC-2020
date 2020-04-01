using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using OGPC;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform groundCheck;

    public LayerMask groundMask;

    public int speed = 1;

    private Camera mainCamera;

    private Tilemap map;

    public GameObject tilemap;

    public GameObject destroySprite;
    public Sprite[] destroySprites;
    public double mineSpeed = 5;

    private double destroyTimer;

    public float jumpCooldownTime = 0.25f;
    public float jumpForce = 550f;
    private bool readyToJump = true;

    public GameObject inventory;
    private bool invEnabled = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        map = tilemap.GetComponent<Tilemap>();
    }

    private void FixedUpdate()
    {
        IsGrounded();

        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetAxis("Jump") > 0.5 && readyToJump)
        {
            rb.AddForce(Vector3.up * jumpForce);
            readyToJump = false;
            Invoke("jumpCooldown", jumpCooldownTime);
        }

        rb.velocity = move;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();

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

    private void IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(1.5f, 0.15f), 0, groundMask))
        {
            readyToJump = true;
        }
        else
        {
            readyToJump = false;
        }
    }

    void jumpCooldown()
    {
        readyToJump = true;
    }

    void ToggleInventory()
    {
        invEnabled = !invEnabled;
        inventory.SetActive(invEnabled);
    }
}
