using UnityEngine;
using UnityEngine.Events;

public class ChildChecker : MonoBehaviour
{
    [Header("Event called when there are no child objects")]
    public UnityEvent OnNoEnemies;
    private bool performed = false;

    [SerializeField] private Animator animator;

    public void Update()
    {
        //Checking for child objects
        if(!performed && transform.childCount == 0)
        {
            performed = true;
            OnNoEnemies.Invoke();

            animator.SetTrigger("Open");
        }
    }
}
