using UnityEngine;
using UnityEngine.Events;

public class ChildChecker : MonoBehaviour
{
    [Header("Event called when there are no child objects")]
    public UnityEvent OnNoEnemies;

    public void Update()
    {
        //Checking for child objects
        if(transform.childCount == 0)
        {
            OnNoEnemies.Invoke();
        }
    }
}
