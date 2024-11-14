using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコア表示用のTextMeshProUGUI

    void Start()
    {
        // スコアを読み込んで表示
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = "あなたのポイント: " + finalScore;
    }
}
