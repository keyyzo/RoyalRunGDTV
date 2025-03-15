using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float increaseTimerAmount = 5.0f;

    const string playerTag = "Player";

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        { 
            gameManager.IncreaseTime(increaseTimerAmount);
        }
    }


}
