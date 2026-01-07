using UnityEngine;
using System.Collections;
public class HitstopManager : MonoBehaviour
{
    
    public static HitstopManager Instance;
    private int hitstopCount = 0;
    private float originalTimeScale = 1f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void DoHitstop(float duration, float timeScale = 0f)
    {
        StartCoroutine(HitstopCoroutine(duration, timeScale));
    }

    private IEnumerator HitstopCoroutine(float duration, float timeScale)
    {
        if (hitstopCount == 0)
        {
            originalTimeScale = Time.timeScale;
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        hitstopCount++;

        yield return new WaitForSecondsRealtime(duration);

        hitstopCount--;
        if (hitstopCount <= 0)
        {
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = 0.02f;
            hitstopCount = 0;
        }
    }
}
