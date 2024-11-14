using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonReactionCounter : MonoBehaviour
{
    public RandomButtonBlink buttonBlinker;
    public TextMeshPro timerText;
    public ScoreManager scoreManager; // ScoreManager �̎Q�Ƃ�ǉ�
    private float timeRemaining = 60f; // 1���i60�b�j
    private bool timerRunning = true;

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        if (!timerRunning)
            return;

        // �K�v�ɉ����đ��̏�����ǉ�
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

        // �Q�[���I�[�o�[�������Ăяo��
        GameOver();
    }

    private void GameOver()
    {
        // ScoreManager ����X�R�A���擾
        int finalScore = scoreManager.GetScore();

        // �X�R�A��ۑ�
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        // �X�R�A�V�[�������[�h
        SceneManager.LoadScene("Score");
    }
}
