using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomButtonBlink : MonoBehaviour
{
    public List<GameObject> buttons;
    public float blinkDuration = 1.0f; // 各ボタンが光る時間
    private GameObject currentBlinkingButton;
    private Coroutine blinkCoroutine;

    void Start()
    {
        // 最初のボタンを光らせる
        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            SetNextBlinkingButton();

            // 指定時間だけ待機し、次のボタンへ移行
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void SetNextBlinkingButton()
    {
        // 現在のボタンをグレーに戻す
        if (currentBlinkingButton != null)
        {
            currentBlinkingButton.GetComponent<Renderer>().material.color = Color.gray;
        }

        // ランダムに新しいボタンを選んで黄色にする
        int index = Random.Range(0, buttons.Count);
        currentBlinkingButton = buttons[index];
        currentBlinkingButton.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public GameObject GetCurrentBlinkingButton()
    {
        return currentBlinkingButton;
    }

    // ボタンがクリックされたときに呼び出す
    public void ButtonClicked()
    {
        // 現在のコルーチンを停止して再スタート
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }
}
