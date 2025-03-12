using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    

    int score = 0;

    private void Start()
    {
        int numOfScoreManagers = FindObjectsByType<ScoreManager>(FindObjectsSortMode.None).Length;

        if (numOfScoreManagers > 1)
        {
            Destroy(gameObject);
        }

        else
        { 
            DontDestroyOnLoad(gameObject);
        }

        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int scoreAmount)
    { 
        score += scoreAmount;

        scoreText.text = "Score: " + score.ToString();
    }
}
