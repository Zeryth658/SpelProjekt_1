using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    
    private Rigidbody2D myRigidbody;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Animator animator;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();

        moveAction = playerInput.actions["Walk"];
    }

    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();

        animator.SetFloat("Movement input", Mathf.Abs(movement.magnitude));

        myRigidbody.linearVelocity = movement * moveSpeed;
    }
}