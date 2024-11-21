using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker;
    public TextMeshPro timerText;
    public ScoreManager scoreManager;
    private float timeRemaining = 60f;
    private bool timerRunning = true;

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        if (!timerRunning)
            return;
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
        int finalScore = scoreManager.GetScore();

        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Score");
    }
}
