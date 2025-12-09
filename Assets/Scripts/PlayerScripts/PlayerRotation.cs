using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : Rotator
{
    [SerializeField] private Transform weapon;

    private Attack attack;

    private void OnLook(InputValue value)
    {
        if(attack.IsAttacking)
            return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        LookAt(mousePosition, weapon.transform);
    }
}
