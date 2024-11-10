using UnityEngine;
using TMPro;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker;
    // public TextMeshPro reactionText;
    public TextMeshPro timerText;
    private int reactionCount = 0;
    private float timeRemaining = 60f; // 1分（60秒）
    private bool timerRunning = true;

    void Start()
    {
        // UpdateReactionText();
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        if (!timerRunning)
            return;

        // マウス左クリックをチェック
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            CheckHitAndCount(ray);
        }
    }

    private void CheckHitAndCount(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedButton = hit.collider.gameObject;

            // 現在光っているボタンと一致する場合のみカウント
            if (clickedButton == buttonBlinker.GetCurrentBlinkingButton())
            {
                // reactionCount++;
                // UpdateReactionText();

                // ボタンクリックを通知し、次のボタンへ移行
                buttonBlinker.ButtonClicked();
            }
        }
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
    }

    /* private void UpdateReactionText()
    {
        reactionText.text = "Count: " + reactionCount;
    } */
}