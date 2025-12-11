using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntryEvent : MonoBehaviour
{
    [Header("Event called when stepping on the trigger")]
    public UnityEvent OnEntryEvent;

    public void OnTriggerEnter2D()
    {
        OnEntryEvent.Invoke();
    }
}
