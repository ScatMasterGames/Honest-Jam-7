using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform footTransform;
    [SerializeField] LayerMask groundLayer;
    float speed = 5.0f;
    float jumpSpeed = 8.0f;
    float gravity = -20.0f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;
            if (Input.GetButton("Jump"))
            {
                move.y = jumpSpeed;
            }

            rb.velocity = move;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(footTransform.position, Vector2.down, 0.1f, groundLayer);
    }
}