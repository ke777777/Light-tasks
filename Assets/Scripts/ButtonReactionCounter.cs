using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker;
    public TextMeshPro timerText;
    public ScoreManager scoreManager; // ScoreManager の参照を追加
    private float timeRemaining = 60f; // 1分（60秒）
    private bool timerRunning = true;

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        if (!timerRunning)
            return;

        // 必要に応じて他の処理を追加
    }

    private IEnumerator TimerCoroutine()
    {
        while (timeRemaining > 0)
        {
            timerText.text = "残り時間: " + Mathf.Ceil(timeRemaining);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        timerRunning = false;
        timerText.text = "残り時間: 0";

        // ゲームオーバー処理を呼び出し
        GameOver();
    }

    private void GameOver()
    {
        // ScoreManager からスコアを取得
        int finalScore = scoreManager.GetScore();

        // スコアを保存
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        // スコアシーンをロード
        SceneManager.LoadScene("Score");
    }
}
