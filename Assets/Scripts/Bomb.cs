using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask playerLayer;
    
    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }
        
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
        Physics2D.OverlapBox(transform.position, new Vector2(1,1),0, playerLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.Die();
        }
    }
}
