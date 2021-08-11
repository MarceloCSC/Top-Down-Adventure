using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private static float hitStop;
    private static float shortPause = 0.1f;
    private static float longPause = 0.2f;
    private static bool isTimeStopped;

    public static TimeManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    public static void HitStop(bool finalBlow)
    {
        hitStop = finalBlow == true ? longPause : shortPause;

        if (!isTimeStopped)
        {
            Instance.StartCoroutine(TimeFreeze());
        }
    }

    private static IEnumerator TimeFreeze()
    {
        Time.timeScale = 0;
        isTimeStopped = true;

        yield return new WaitForSecondsRealtime(hitStop);

        Time.timeScale = 1;
        isTimeStopped = false;
    }

}
