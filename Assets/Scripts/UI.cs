using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static UI instance;

    private float clearTimer;
    private bool timerRunning;

    private void Awake()
    {
        instance = this;
        ClearSubtitle();
    }

    private void Update()
    {
        if (!timerRunning) return;

        clearTimer -= Time.deltaTime;

        if(clearTimer <= 0f)
        {
            ClearSubtitle();
            timerRunning = false;
        }
    }

    public void SetSubtitle(string subtitle, float delay)
    {
        subtitleText.text = subtitle;
        clearTimer = delay;
        timerRunning = true;
    }

    public void ClearSubtitle()
    {
        subtitleText.text = "";
        timerRunning = false;
    }
}
