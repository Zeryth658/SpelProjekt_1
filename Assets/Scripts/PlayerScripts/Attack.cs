using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject melee;

    private PlayerController controls;
    private Animator animator;

    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

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
        CheckMeleeTimer();
    }

    private void OnAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            melee.SetActive(true);
        }
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
