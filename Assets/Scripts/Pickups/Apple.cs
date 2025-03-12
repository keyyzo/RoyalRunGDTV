using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float powerUpSpeed = 3.0f;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    protected override void OnPickup()
    {
        Debug.Log("Power up!");
        levelGenerator.ChangeChunkMoveSpeed(powerUpSpeed);
    }
}
