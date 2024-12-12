using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    int score = 0;
    public int Score => score;
    public UnityEvent OnScoreChanged;
    
    public static GameManager Instance;

    public bool IsGameOver => gameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        OnScoreChanged.Invoke();
    }
    public void WinGame()
    {
        gameOver = true;
        Invoke(nameof(RestartGame), 2);
    }
    void RestartGame()
    {
        gameOver = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void Die()
    {
        gameOver = true;
        Invoke(nameof(RestartGame), 2);
    }
}