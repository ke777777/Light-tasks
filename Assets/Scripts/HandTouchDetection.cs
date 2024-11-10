using UnityEngine;
using TMPro;
using OculusSampleFramework;

public class HandTouchDetection : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton;
    public RandomButtonBlink buttonManager;
    public TextMeshPro scoreText; // �X�R�A�\���p��TMP
    private int score = 0;
    private Transform indexTipTransform;
    private GameObject lastTouchedButton;

    void Start()
    {
        indexTipTransform = FindFingerTip(OVRSkeleton.BoneId.Hand_IndexTip);

        if (indexTipTransform == null)
        {
            Debug.LogError("Error: �w��{�[����������܂���B");
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (indexTipTransform == null)
            return;

        // �w��̈ʒu����Ray���΂��ă{�^���ƐڐG���m�F
        Ray ray = new Ray(indexTipTransform.position, indexTipTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.2f))
        {
            GameObject touchedButton = hit.collider.gameObject;

            // �����Ă���{�^���ƈ�v����ꍇ�̂݃J�E���g
            if (touchedButton.CompareTag("Button") && touchedButton == buttonManager.GetCurrentBlinkingButton())
            {
                if (lastTouchedButton != touchedButton)
                {
                    score++; // �^�b�`�����Ƃ��ɃX�R�A�𑝂₷
                    UpdateScoreText(); // �X�R�A�̕\�����X�V
                    lastTouchedButton = touchedButton;

                    // ���̃{�^���Ɉڍs
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
