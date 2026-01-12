using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;

    public void Freeze()
    {
        movement.Toggle_Freeze();
    }
}
