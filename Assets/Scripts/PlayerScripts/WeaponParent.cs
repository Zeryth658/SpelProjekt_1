using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class WeaponParent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer, weaponRenderer;

    public Vector2 PointerPosition { get; set; }

    public Animator animator;
    [SerializeField] private float delay = 0.4f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set;}

    [Header("Add Your Sounds Here")]
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    private AudioSource audioSource;
    private List<AudioClip> shuffleSounds;
    private int currentIndex = 0;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Awake()
    {
        ShuffleSounds();
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
        PlayRandomSound();
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    public void PlayRandomSound()
    {
        if(sounds.Count == 0)
        {
            Debug.LogWarning("No sounds assigned.");
            return;
        }

        if(currentIndex >= shuffleSounds.Count)
        {
            ShuffleSounds();
        }

        audioSource.PlayOneShot(shuffleSounds[currentIndex]);
        currentIndex++;
    }

    private void ShuffleSounds()
    {
        shuffleSounds = new List<AudioClip>(sounds);

        for (int i = 0; i < shuffleSounds.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffleSounds.Count);
            (shuffleSounds[i], shuffleSounds[randomIndex]) =
                (shuffleSounds[randomIndex], shuffleSounds[i]);
        }

        currentIndex = 0;
    }
}
