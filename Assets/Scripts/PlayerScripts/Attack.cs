using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer, weaponRenderer;


    private float delay = 0.4f;
    private bool attackBlocked;

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
        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    private void OnAttack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    public void StartAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
}
