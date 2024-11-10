using UnityEngine;
using TMPro;
using OculusSampleFramework;

public class HandTouchDetection : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton;
    public RandomButtonBlink buttonManager;
    public TextMeshPro scoreText; // スコア表示用のTMP
    private int score = 0;
    private Transform indexTipTransform;
    private GameObject lastTouchedButton;

    void Start()
    {
        indexTipTransform = FindFingerTip(OVRSkeleton.BoneId.Hand_IndexTip);

        if (indexTipTransform == null)
        {
            Debug.LogError("Error: 指先ボーンが見つかりません。");
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (indexTipTransform == null)
            return;

        // 指先の位置からRayを飛ばしてボタンと接触を確認
        Ray ray = new Ray(indexTipTransform.position, indexTipTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.2f))
        {
            GameObject touchedButton = hit.collider.gameObject;

            // 光っているボタンと一致する場合のみカウント
            if (touchedButton.CompareTag("Button") && touchedButton == buttonManager.GetCurrentBlinkingButton())
            {
                if (lastTouchedButton != touchedButton)
                {
                    score++; // タッチしたときにスコアを増やす
                    UpdateScoreText(); // スコアの表示を更新
                    lastTouchedButton = touchedButton;

                    // 次のボタンに移行
                    buttonManager.ButtonClicked();
                }
            }
        }
        else
        {
            lastTouchedButton = null;
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

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
