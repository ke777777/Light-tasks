using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomButtonBlink : MonoBehaviour
{
    public List<GameObject> buttons;         // ボタンのリスト
    private GameObject currentBlinkingButton; // 現在光っているボタン

    void Start()
    {
        // 最初のボタンを光らせる
        SetNextBlinkingButton();
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

    // ボタンがタッチされたときに呼び出す
    public void ButtonClicked()
    {
        // タッチされた際に次のボタンへ移行
        SetNextBlinkingButton();
    }
}
