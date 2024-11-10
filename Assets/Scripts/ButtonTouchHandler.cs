using UnityEngine;

public class ButtonTouchHandler : MonoBehaviour
{
    public RandomButtonBlink buttonManager; // ランダムに光るボタンの管理
    private bool isTouched = false;         // タッチ済みかどうかを判定

    void OnTriggerEnter(Collider other)
    {
        // 手がボタンに触れ、光っているボタンに一致する場合のみカウント
        if (other.CompareTag("Hand") && buttonManager.GetCurrentBlinkingButton() == gameObject && !isTouched)
        {
            isTouched = true;
            Debug.Log("Button touched!");

            // タッチしたら反応をカウントし、次のボタンへ移行
            buttonManager.ButtonClicked();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 手が離れたら再度タッチできるようにリセット
        if (other.CompareTag("Hand"))
        {
            isTouched = false;
        }
    }
}
