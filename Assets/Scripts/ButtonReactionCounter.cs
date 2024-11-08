using UnityEngine;
using TMPro;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker; // RandomButtonBlink�X�N���v�g�ւ̎Q��
    public TextMeshPro reactionText;
    public TextMeshPro timerText; // �^�C�}�[�\���p�̃e�L�X�g
    private int reactionCount = 0;
    private float timeRemaining = 60f; // 1���i60�b�j
    private bool timerRunning = true;

    void Start()
    {
        UpdateReactionText();
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        if (!timerRunning)
            return;

        // �R���g���[���[��A�{�^�����͂��`�F�b�N
        if (OVRInput.GetDown(OVRInput.Button.One)) // A�{�^���iOculus�R���g���[���[�j
        {
            Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);
            CheckHitAndCount(ray);
        }

        // �}�E�X���N���b�N���`�F�b�N
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            CheckHitAndCount(ray);
        }
    }

    // �{�^�����N���b�N���ꂽ�ꍇ�ɃJ�E���g�𑝂₷
    private void CheckHitAndCount(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedButton = hit.collider.gameObject;

            // ���݌����Ă���{�^���ƈ�v����ꍇ�̂݃J�E���g
            if (clickedButton == buttonBlinker.GetCurrentBlinkingButton())
            {
                reactionCount++;
                UpdateReactionText();

                // �{�^���N���b�N��ʒm���A���̃{�^���ֈڍs
                buttonBlinker.ButtonClicked();
            }
        }
    }

    // �^�C�}�[�̃J�E���g�_�E��
    private IEnumerator TimerCoroutine()
    {
        while (timeRemaining > 0)
        {
            timerText.text = "�c�莞��: " + Mathf.Ceil(timeRemaining);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        // �^�C�}�[���I��������J�E���g���~
        timerRunning = false;
        timerText.text = "�c�莞��: 0";
    }

    private void UpdateReactionText()
    {
        reactionText.text = "Count: " + reactionCount;
    }
}
