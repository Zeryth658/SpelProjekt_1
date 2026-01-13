using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EntryEvent : MonoBehaviour
{
    [Header("Event called when stepping on the trigger")]
    public UnityEvent OnEntryEvent;
    private TagHandle playerTag;

    [SerializeField] private Animator animator;

    public void OnEnable()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            OnEntryEvent.Invoke();

            animator.SetTrigger("Close");
        }
    }
}
