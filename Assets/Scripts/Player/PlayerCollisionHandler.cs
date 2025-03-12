using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float hitCooldownLength = 1f;
    [SerializeField] float adjustChangeMoveSpeed = -2f;


    const string hitString = "Hit";

    float hitCooldownTimer = 0.0f;
    bool isHitOnCooldown = false;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void Update()
    {
        if (isHitOnCooldown)
        {
            hitCooldownTimer += Time.deltaTime;

            if (hitCooldownTimer >= hitCooldownLength)
            {
                isHitOnCooldown = false;
            }
        }

        else
        {
            hitCooldownTimer = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isHitOnCooldown)
        {
            isHitOnCooldown = true;
            animator.SetTrigger(hitString);
            levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeed);
        }
        
    }
}
