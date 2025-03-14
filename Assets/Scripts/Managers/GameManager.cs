using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] float startingTime = 5.0f;
    [SerializeField] float gameOverFadeInMultiplier = 2.0f;

    float timeLeft;
    bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private void Start()
    {
        timeLeft = startingTime;

    }

    private void Update()
    {
        
        DecreaseTime();

        if (timeLeft <= 0.0f)
        {
            GameOver();  
            FadeInGameOver();
        }
    }

    void DecreaseTime()
    {
        if (!isGameOver)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = timeLeft.ToString("F2") + " : Time";
        }


        
    }

    void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false;
        timeLeft = 0.0f;
        

        Time.timeScale = 0.1f;

        
        
    }

    void FadeInGameOver()
    {
        gameOverText.gameObject.SetActive(true);

        if (gameOverText.alpha < 1.0f)
        {
            gameOverText.alpha += (Time.deltaTime * gameOverFadeInMultiplier);
        }
    }
}
