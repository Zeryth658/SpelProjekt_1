using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] public float maxSpeed = 5f, acceleration = 50f, deacceleration = 100;
    [SerializeField] public float currentSpeed = 0f;
    [SerializeField] private float stickAimDistance = 2f;
    [SerializeField] private float stickDeadzone = 0.2f;
    
    public float standardMaxSpeed;

    [SerializeField] private InputActionReference movement, attack, pointerPosition, lookStick;
    
    private Vector2 pointerInput, movementInput;

    private WeaponParent weaponParent;
    private PlayerHealth playerHealth;
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private DodgeRoll dodgeRoll;
    [SerializeField] private ParticleSystem dustParticles;

    private enum AimSource
    {
        Mouse,
        Controller
    }
    private AimSource currentAimSource = AimSource.Mouse;
    private Vector2 lastMousePosition;
    private Vector2 lastStickDirection;
    
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
        if(PauseMenu.IsPaused) { return; }
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

        standardMaxSpeed = maxSpeed;
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

        if (movementInput.magnitude > 0)
        {
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            if(!dustParticles.isPlaying) { dustParticles.Play(); }
        }
        else if (currentSpeed >= 0.1)
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            if (dustParticles.isPlaying) { dustParticles.Stop(); }
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        myRigidbody.linearVelocity = movementInput * currentSpeed;
    }

    private Vector2 GetPointerInput()
    {
        Vector2 stickInput = lookStick.action.ReadValue<Vector2>();
        Vector2 mouseScreenPos = pointerPosition.action.ReadValue<Vector2>();

        if (stickInput.magnitude > stickDeadzone)
        {
            lastStickDirection = stickInput.normalized;
            currentAimSource = AimSource.Controller;
        }

        if ((mouseScreenPos - lastMousePosition).sqrMagnitude > 1f)
        {
            currentAimSource = AimSource.Mouse;
            lastMousePosition = mouseScreenPos;
        }

        if (currentAimSource == AimSource.Controller)
        {
            return (Vector2)transform.position + lastStickDirection * stickAimDistance;
        }
        else
        {
            Vector3 mousePos = mouseScreenPos;
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}