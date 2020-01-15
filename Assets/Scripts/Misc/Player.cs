using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public int speed = 1;

    private bool onGround = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onGround = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        onGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onGround = false;
    }
}
