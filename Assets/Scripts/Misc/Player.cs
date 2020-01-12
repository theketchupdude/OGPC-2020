using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;

    public int Speed = 1;

    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        character.Move(move * Time.deltaTime * Speed);
        if (move != Vector3.zero)
            transform.forward = move;
    }
}
