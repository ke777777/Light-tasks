using UnityEngine;

public class ButtonTouchHandler : MonoBehaviour
{
    public RandomButtonBlink buttonManager; // �����_���Ɍ���{�^���̊Ǘ�
    private bool isTouched = false;         // �^�b�`�ς݂��ǂ����𔻒�

    void OnTriggerEnter(Collider other)
    {
        // �肪�{�^���ɐG��A�����Ă���{�^���Ɉ�v����ꍇ�̂݃J�E���g
        if (other.CompareTag("Hand") && buttonManager.GetCurrentBlinkingButton() == gameObject && !isTouched)
        {
            isTouched = true;
            Debug.Log("Button touched!");

            // �^�b�`�����甽�����J�E���g���A���̃{�^���ֈڍs
            buttonManager.ButtonClicked();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �肪���ꂽ��ēx�^�b�`�ł���悤�Ƀ��Z�b�g
        if (other.CompareTag("Hand"))
        {
            isTouched = false;
        }
    }
}
