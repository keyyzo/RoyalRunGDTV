using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;


    ScoreManager scoreManager;


    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    protected override void OnPickup()
    {
        Debug.Log("Add 100 Points");
        scoreManager.IncreaseScore(scoreAmount);
    }
}
