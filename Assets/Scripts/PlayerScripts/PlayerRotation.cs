using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    private void LookAt(Vector3 target)
    {
        float lookAngle = AngleBetweenTwoPoints(transform.position, target) + 90;

        transform.eulerAngles = new Vector3(0, 0, lookAngle);
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnLook(InputValue value)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        LookAt(mousePosition);
    }
}
