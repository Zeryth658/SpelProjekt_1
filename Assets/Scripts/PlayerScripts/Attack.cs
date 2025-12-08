using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private float attackTimer;
    private float attackCooldown = 0.4f;

    private PlayerController controls;
    private Animator animator;
    [SerializeField] private Transform weaponCollider;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerController();
    }
    
    private void OnEnable() => controls.Enable();

    void Start()
    {
        controls.Combat.Attack.started += _ => OnAttack();
    }

    // Update is called once per frame
    private void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void OnAttack()
    {
        if (attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void StartAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
        
        attackTimer = attackCooldown;
    }
}
