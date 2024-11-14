using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // �X�R�A�\���p��TextMeshProUGUI

    void Start()
    {
        // �X�R�A��ǂݍ���ŕ\��
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = "���Ȃ��̃|�C���g: " + finalScore;
    }
}
