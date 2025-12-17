using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponParent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer, weaponRenderer;

    public Vector2 PointerPosition { get; set; }

    public Animator animator;
    [SerializeField] private float delay = 0.4f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set;}

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    void Update()
    {
        if(IsAttacking) { return; }
        
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    public void OnAttack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        SoundManager.PlaySound(SoundType.Sword);
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
