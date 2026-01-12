using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    public static bool canFade = true;
    public void Freeze()
    {
        movement.Toggle_Freeze();
    }

    private void Update()
    {
        if(canFade == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void Fade()
    {
        canFade = !canFade;
    }

}
