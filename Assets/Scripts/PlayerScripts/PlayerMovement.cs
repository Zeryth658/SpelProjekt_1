using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] public float maxSpeed = 5f, acceleration = 50f, deacceleration = 100;
    [SerializeField] public float currentSpeed = 0f;

    [SerializeField] private InputActionReference movement, attack, pointerPosition;
    
    private Vector2 pointerInput, movementInput;

    private WeaponParent weaponParent;
    private PlayerHealth playerHealth;
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private DodgeRoll dodgeRoll;

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if(playerHealth.isDead == true) { return; }
        weaponParent.OnAttack();
    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponentInChildren<Animator>();
        dodgeRoll = GetComponent<DodgeRoll>();
    }

    private void FixedUpdate()
    {
        if (dodgeRoll.isDodging)
        {
            return;
        }
        if(playerHealth.isDead == true) 
        { 
            movementInput = Vector2.zero;
            myRigidbody.linearVelocity = movementInput * currentSpeed;
            return;
        }
        
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>().normalized;

        Vector2 lookDirection = (pointerInput - (Vector2)transform.position).normalized;

        animator.SetFloat("Movement input", Mathf.Abs(movementInput.magnitude));
        
        if (characterRenderer != null)
        {
            if(lookDirection.x < 0)
            {
                characterRenderer.flipX = true;
            }
            else if (lookDirection.x > 0)
            {
                characterRenderer.flipX = false;
            }
        }

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