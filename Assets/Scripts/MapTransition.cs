using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    PolygonCollider2D mapBoundary;
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float addNewPos = 2;

    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        mapBoundary = GetComponentInParent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += addNewPos;
                break;
            case Direction.Down:
                newPos.y -= addNewPos;
                break;
            case Direction.Left:
                newPos.x += addNewPos;
                break;
            case Direction.Right:
                newPos.x -= addNewPos;
                break;
        }

        player.transform.position = newPos;
    }
}
