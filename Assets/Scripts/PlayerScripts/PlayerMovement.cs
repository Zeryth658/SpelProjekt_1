using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f, acceleration = 50f, deacceleration = 100;
    [SerializeField] private float currentSpeed = 0f;

    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput => pointerInput;

    [SerializeField] private InputActionReference movement, pointerPosition;

    private Rigidbody2D myRigidbody;
    private Animator animator;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        pointerInput = GetPointerInput();
        
        movementInput = movement.action.ReadValue<Vector2>();

        animator.SetFloat("Movement input", Mathf.Abs(movementInput.magnitude));

        if (movementInput.magnitude > 0 && currentSpeed >= 0)
        {
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        myRigidbody.linearVelocity = movementInput * currentSpeed;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}