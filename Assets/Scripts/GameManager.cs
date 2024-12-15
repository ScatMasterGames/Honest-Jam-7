using System.Linq;
using TMPro;
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
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text winText;

    public bool IsGameOver => gameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            var texts = FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);


            gameOverText = texts.FirstOrDefault(t => t.gameObject.name == "Game Over Text");
            winText = texts.FirstOrDefault(t => t.gameObject.name == "Victory Text");

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
        winText.gameObject.SetActive(true);
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
        gameOverText.gameObject.SetActive(true);
    }
}