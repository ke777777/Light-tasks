using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText; // スコア表示用のTMP
    private int score = 0;

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
