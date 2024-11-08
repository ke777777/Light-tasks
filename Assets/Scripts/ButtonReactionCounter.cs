using UnityEngine;
using TMPro;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker; // RandomButtonBlinkスクリプトへの参照
    public TextMeshPro reactionText;
    private int reactionCount = 0;

    void Start()
    {
        UpdateReactionText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedButton = hit.collider.gameObject;

                // 現在光っているボタンと一致する場合のみカウント
                if (clickedButton == buttonBlinker.GetCurrentBlinkingButton())
                {
                    reactionCount++;
                    UpdateReactionText();

                    // ボタンクリックを通知し、次のボタンへ移行
                    buttonBlinker.ButtonClicked();
                }
            }
        }
    }

    private void UpdateReactionText()
    {
        reactionText.text = "Count: " + reactionCount;
    }
}
