using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject melee;

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
        
    }

    private void OnAttack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
}
