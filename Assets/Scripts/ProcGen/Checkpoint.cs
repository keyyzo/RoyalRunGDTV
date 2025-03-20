using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float increaseTimerAmount = 5.0f;
    [SerializeField] float obstacleDecreaseTimeAmount = 0.25f;

    const string playerTag = "Player";

    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        { 
            gameManager.IncreaseTime(increaseTimerAmount);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }


}
