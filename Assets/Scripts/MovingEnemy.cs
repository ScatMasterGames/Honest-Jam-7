using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] private float xPositiveLimitOffset;
    [SerializeField] private float xNegativeLimitOffset;
    [SerializeField] SpriteRenderer spriteRenderer;
    float xPositiveLimit;
    float xNegativeLimit;
    
    private void Start()
    {
        xPositiveLimit = transform.position.x + xPositiveLimitOffset;
        xNegativeLimit = transform.position.x + xNegativeLimitOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //Move back and forth
        if (transform.position.x < xNegativeLimit)
        {
            moveSpeed = Mathf.Abs(moveSpeed);
            spriteRenderer.flipX = moveSpeed > 0;
        }
        else if (transform.position.x > xPositiveLimit)
        {
            moveSpeed = -Mathf.Abs(moveSpeed);
            spriteRenderer.flipX = moveSpeed > 0;
        }
        
        transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x+xNegativeLimitOffset, transform.position.y), new Vector3(xPositiveLimitOffset+transform.position.x, transform.position.y));
    }
}
