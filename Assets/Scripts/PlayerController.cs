using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Variables")]

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float positionXClamp = 4.5f;
    [SerializeField] float positionZClamp = 5f;

    Vector2 movement;


    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        
    }

    void HandleMovement()
    { 
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);

        float clampedXPos = Mathf.Clamp(currentPosition.x, -positionXClamp, positionXClamp);
        float clampedZPos = Mathf.Clamp(currentPosition.z, -positionZClamp, positionZClamp);

        Vector3 clampedPosition = new Vector3(clampedXPos, currentPosition.y, clampedZPos);

        Vector3 newPosition = clampedPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);



        rb.MovePosition(newPosition);
    }

}
