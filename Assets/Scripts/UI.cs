using System.Collections;
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
        ClearSubtitle();
    }

    public void SetSubtitle(string subtitle, float delay)
    {
        subtitleText.text = subtitle;

        StartCoroutine(ClearAfterSeconds(delay));
    }

    public void ClearSubtitle()
    {
        subtitleText.text = "";
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }
}
