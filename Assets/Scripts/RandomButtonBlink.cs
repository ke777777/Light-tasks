using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomButtonBlink : MonoBehaviour
{
    public List<GameObject> buttons;         // �{�^���̃��X�g
    private GameObject currentBlinkingButton; // ���݌����Ă���{�^��

    void Start()
    {
        // �ŏ��̃{�^�������点��
        SetNextBlinkingButton();
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

    // �{�^�����^�b�`���ꂽ�Ƃ��ɌĂяo��
    public void ButtonClicked()
    {
        // �^�b�`���ꂽ�ۂɎ��̃{�^���ֈڍs
        SetNextBlinkingButton();
    }
}
