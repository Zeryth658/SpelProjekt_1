using UnityEngine;
using UnityEngine.Events;

public class ChildChecker : MonoBehaviour
{
    [Header("Event called when there are no child objects")]
    public UnityEvent OnNoEnemies;
    private bool performed = false;

    public void Update()
    {
        //Checking for child objects
        if(!performed && transform.childCount == 0)
        {
            performed = true;
            OnNoEnemies.Invoke();
        }
    }
}
