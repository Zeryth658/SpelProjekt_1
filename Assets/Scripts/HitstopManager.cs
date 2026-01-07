using UnityEngine;
using System.Collections;
public class HitstopManager : MonoBehaviour
{
    
    public static HitstopManager Instance;
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
        float originalTimeScale = Time.timeScale;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;
    }
}
