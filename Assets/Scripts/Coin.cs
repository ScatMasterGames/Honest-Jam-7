using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin :MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    public UnityEvent OnCoinCollected;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            OnCoinCollected.Invoke();
            _renderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
            
            
        }
    }
}