using UnityEngine;
using OculusSampleFramework;

public class HandTouchHandler : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton;
    public RandomButtonBlink buttonManager;
    public ScoreManager scoreManager; // ScoreManager への参照
    private Transform indexTipTransform;
    private GameObject lastBlinkingButton;
    private bool isTouched = false; // タッチ済みかどうかを判定

    void Start()
    {
        indexTipTransform = FindFingerTip(OVRSkeleton.BoneId.Hand_IndexTip);

        if (indexTipTransform == null)
        {
            Debug.LogError("Error: 指先ボーンが見つかりません。");
        }

        lastBlinkingButton = buttonManager.GetCurrentBlinkingButton();
    }

    void Update()
    {
        if (indexTipTransform == null)
            return;

        // 現在の光っているボタンを取得
        GameObject currentBlinkingButton = buttonManager.GetCurrentBlinkingButton();

        // 光っているボタンが変わった場合、タッチ状態をリセット
        if (currentBlinkingButton != lastBlinkingButton)
        {
            isTouched = false;
            lastBlinkingButton = currentBlinkingButton;
        }

        // 指先の位置からSphereCastを使ってボタンと接触を確認
        Ray ray = new Ray(indexTipTransform.position, -indexTipTransform.up);
        RaycastHit hit;

        // SphereCastを使用して当たり判定を広げる
        float sphereRadius = 0.02f;
        if (Physics.SphereCast(ray, sphereRadius, out hit, 0.05f))
        {
            GameObject touchedButton = hit.collider.gameObject;

            // 光っているボタンと一致する場合のみカウント
            if (touchedButton.CompareTag("Button") && touchedButton == currentBlinkingButton)
            {
                if (!isTouched)
                {
                    isTouched = true;
                    scoreManager.AddScore(1);

                    // 次のボタンに移行
                    buttonManager.ButtonClicked();
                }
            }
        }
        else
        {
            isTouched = false;
        }
    }

    private Transform FindFingerTip(OVRSkeleton.BoneId boneId)
    {
        foreach (var bone in ovrSkeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                return bone.Transform;
            }
        }
        return null;
    }
}
