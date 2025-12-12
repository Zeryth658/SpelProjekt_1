using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntryEvent : MonoBehaviour
{
    [Header("Event called when stepping on the trigger")]
    public UnityEvent OnEntryEvent;
    
    private TagHandle playerTag;

    public void OnEnable()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }

    public void OnTriggerEnter2D(GameObject other)
    {
        if (other.CompareTag(playerTag))
        {
            OnEntryEvent.Invoke();
        }
    }
}
