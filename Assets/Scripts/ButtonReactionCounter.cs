using UnityEngine;
using TMPro;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker; // RandomButtonBlink�X�N���v�g�ւ̎Q��
    public TextMeshPro reactionText;
    private int reactionCount = 0;

    void Start()
    {
        UpdateReactionText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
    }

    private void UpdateReactionText()
    {
        reactionText.text = "Count: " + reactionCount;
    }
}
