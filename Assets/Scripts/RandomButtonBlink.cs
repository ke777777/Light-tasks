using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomButtonBlink : MonoBehaviour
{
    public List<GameObject> buttons;
    public float blinkDuration = 1.0f; // �e�{�^�������鎞��
    private GameObject currentBlinkingButton;
    private Coroutine blinkCoroutine;

    void Start()
    {
        // �ŏ��̃{�^�������点��
        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            SetNextBlinkingButton();

            // �w�莞�Ԃ����ҋ@���A���̃{�^���ֈڍs
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void SetNextBlinkingButton()
    {
        // ���݂̃{�^�����O���[�ɖ߂�
        if (currentBlinkingButton != null)
        {
            currentBlinkingButton.GetComponent<Renderer>().material.color = Color.gray;
        }

        // �����_���ɐV�����{�^����I��ŉ��F�ɂ���
        int index = Random.Range(0, buttons.Count);
        currentBlinkingButton = buttons[index];
        currentBlinkingButton.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public GameObject GetCurrentBlinkingButton()
    {
        return currentBlinkingButton;
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo��
    public void ButtonClicked()
    {
        // ���݂̃R���[�`�����~���čăX�^�[�g
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }
}
