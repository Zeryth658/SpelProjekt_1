using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    
    private Rigidbody2D myRigidbody;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Walk"];
    }

    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();

        myRigidbody.linearVelocity = movement * moveSpeed;
    }
}