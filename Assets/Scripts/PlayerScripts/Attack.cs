using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject melee;

    private PlayerInput playerInput;
    private InputAction attackAction;
    private Animator animator;

    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        
        attackAction = playerInput.actions["Attack"];
        attackAction.performed += OnAttackInput;
    }

    private void OnEnable() => attackAction.Enable();
    private void OnDisable() => attackAction.Disable();

    // Update is called once per frame
    private void Update()
    {
        CheckMeleeTimer();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            OnAttack();
        }
    }

    private void OnAttack()
    {
        isAttacking = true;
        //animator.SetTrigger("Attack");
        melee.SetActive(true);
    }

    private void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                isAttacking = false;
                atkTimer = 0;
                melee.SetActive(false);
            }
        }
    }
}
