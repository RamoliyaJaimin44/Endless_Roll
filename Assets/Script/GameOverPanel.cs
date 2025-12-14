using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public PlayerController playerController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public Button restartBtn;

    private void OnEnable()
    {
        AudioManager.Instance.LoseSound();
        scoreText.text = "Score :" + playerController.score.ToString("F1") + " m";
        if (PlayerPrefs.HasKey("BestScore"))
        {
            float BestScore = PlayerPrefs.GetFloat("BestScore");
            if (playerController.score >= BestScore) 
            {
                bestScoreText.text = "Best Score :" + playerController.score.ToString("F1") + " m";
                PlayerPrefs.SetFloat("BestScore", playerController.score);
            }
            else
            {
                bestScoreText.text = "Best Score :" + BestScore.ToString("F1") + " m";
            }
        }
        else
        {
            bestScoreText.text = "Best Score :" + playerController.score.ToString("F1") + " m";
            PlayerPrefs.SetFloat("BestScore", playerController.score);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        restartBtn.onClick.AddListener(RestartBtnClick);
    }

    void RestartBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
