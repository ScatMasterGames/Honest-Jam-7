using UnityEngine;
using UnityEngine.Events;

public class Coin :MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    public UnityEvent OnCoinCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            OnCoinCollected.Invoke();
            gameObject.SetActive(false);
        }
    }
}