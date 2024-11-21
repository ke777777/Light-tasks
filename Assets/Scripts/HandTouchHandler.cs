using UnityEngine;
using OculusSampleFramework;

public class HandTouchHandler : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton;
    public RandomButtonBlink buttonManager;
    public ScoreManager scoreManager; // ScoreManager �ւ̎Q��
    private Transform indexTipTransform;
    private GameObject lastBlinkingButton;
    private bool isTouched = false; // �^�b�`�ς݂��ǂ����𔻒�

    void Start()
    {
        indexTipTransform = FindFingerTip(OVRSkeleton.BoneId.Hand_IndexTip);

        if (indexTipTransform == null)
        {
            Debug.LogError("Error: �w��{�[����������܂���B");
        }

        lastBlinkingButton = buttonManager.GetCurrentBlinkingButton();
    }

    void Update()
    {
        if (indexTipTransform == null)
            return;

        // ���݂̌����Ă���{�^�����擾
        GameObject currentBlinkingButton = buttonManager.GetCurrentBlinkingButton();

        // �����Ă���{�^�����ς�����ꍇ�A�^�b�`��Ԃ����Z�b�g
        if (currentBlinkingButton != lastBlinkingButton)
        {
            isTouched = false;
            lastBlinkingButton = currentBlinkingButton;
        }

        // �w��̈ʒu����SphereCast���g���ă{�^���ƐڐG���m�F
        Ray ray = new Ray(indexTipTransform.position, -indexTipTransform.up);
        RaycastHit hit;

        // SphereCast���g�p���ē����蔻����L����
        float sphereRadius = 0.02f;
        if (Physics.SphereCast(ray, sphereRadius, out hit, 0.05f))
        {
            GameObject touchedButton = hit.collider.gameObject;

            // �����Ă���{�^���ƈ�v����ꍇ�̂݃J�E���g
            if (touchedButton.CompareTag("Button") && touchedButton == currentBlinkingButton)
            {
                if (!isTouched)
                {
                    isTouched = true;
                    scoreManager.AddScore(1);

                    // ���̃{�^���Ɉڍs
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
