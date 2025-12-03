using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    Collider waypoint;
    PolygonCollider2D mapBoundary;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float addNewPos = 2;
    private bool hasEntered = false;

    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        waypoint = GetComponent<Collider>();
        mapBoundary = GetComponentInParent<PolygonCollider2D>();
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasEntered == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                confiner.BoundingShape2D = mapBoundary;
                UpdatePlayerPosition(collision.gameObject);
                waypoint.isTrigger = false;
                hasEntered = true;
            }
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
                newPos.x -= addNewPos;
                break;
            case Direction.Right:
                newPos.x += addNewPos;
                break;
        }

        player.transform.position = newPos;
    }
}
