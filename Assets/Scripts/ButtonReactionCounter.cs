using UnityEngine;
using TMPro;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker;
    // public TextMeshPro reactionText;
    public TextMeshPro timerText;
    private int reactionCount = 0;
    private float timeRemaining = 60f; // 1���i60�b�j
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

        // �}�E�X���N���b�N���`�F�b�N
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
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

            // ���݌����Ă���{�^���ƈ�v����ꍇ�̂݃J�E���g
            if (clickedButton == buttonBlinker.GetCurrentBlinkingButton())
            {
                // reactionCount++;
                // UpdateReactionText();

                // �{�^���N���b�N��ʒm���A���̃{�^���ֈڍs
                buttonBlinker.ButtonClicked();
            }
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (timeRemaining > 0)
        {
            timerText.text = "�c�莞��: " + Mathf.Ceil(timeRemaining);
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        timerRunning = false;
        timerText.text = "�c�莞��: 0";
    }

    /* private void UpdateReactionText()
    {
        reactionText.text = "Count: " + reactionCount;
    } */
}