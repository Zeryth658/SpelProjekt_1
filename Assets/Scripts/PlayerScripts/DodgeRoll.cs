using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class DodgeRoll : MonoBehaviour
{

    public float dodgeSpeed = 12f;
    public float dodgeDuration = 0.4f;
    public float dodgeCooldown = 0.4f;
    public AnimationCurve SpeedCurve;
    [SerializeField] private PlayerInput playerInput;
    private InputAction _dodgeAction;
    private Vector2 moveInput;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;
    public bool isDodging { get; private set; }
    private bool _canDodge = true;


    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _dodgeAction = playerInput.actions["Dodge"];
    }
    
    private void OnEnable()
    {
        _dodgeAction.performed += OnDodgePerformed;
        _dodgeAction.Enable();
    }

    private void OnDisable()
    {
        _dodgeAction.performed -= OnDodgePerformed;
        _dodgeAction.Disable();
    }

    private void OnDodgePerformed(InputAction.CallbackContext context)
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (!isDodging && _canDodge && moveInput.magnitude > 0.1f)
        {
            
            StartCoroutine(Dodge(moveInput));
        }
    }

    private IEnumerator Dodge(Vector2 input)
    {
        _canDodge = false;
        isDodging = true;
        _playerHealth.immune = true;
        float time = 0;

        while (time < dodgeDuration)
        {
            float timeCurve = time / dodgeDuration;
            float speed = dodgeSpeed * SpeedCurve.Evaluate(timeCurve);
            
            _rigidbody.linearVelocity = input * speed;

            if (time >= dodgeDuration / 2)
            {
                _playerHealth.immune = false;
            }
            time += Time.deltaTime;
            
            yield return null;
        }
        _rigidbody.linearVelocity = Vector2.zero;
        isDodging = false;
        
        yield return new WaitForSeconds(dodgeCooldown);
        _canDodge = true;
    }
    
}
