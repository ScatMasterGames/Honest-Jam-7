using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    
    private void Start()
    {
        GameManager.Instance.OnScoreChanged.AddListener(UpdateScore);
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.SetText($"Score: {GameManager.Instance.Score}");
    }
}
