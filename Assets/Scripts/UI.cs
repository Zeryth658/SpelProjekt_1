using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetSubtitle(string subtitle)
    {
        subtitleText.text = subtitle;
    }
}
