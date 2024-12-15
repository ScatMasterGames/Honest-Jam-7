
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour
{
  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.Die();
        }
    }

 
}
