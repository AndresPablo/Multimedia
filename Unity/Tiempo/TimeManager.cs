using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayTime{

public class TimeManager : MonoBehaviour
{
    public float playTime;
    public float totalTime;
    int logOffTime;

    public int timeOffline;

    void Start()
    {
        playTime = PlayerPrefs.GetFloat("playTime");
        totalTime = PlayerPrefs.GetFloat("totalTime");
        logOffTime = PlayerPrefs.GetInt("logOffTime");

        int loginTime = (int)System.DateTime.UtcNow.Second;
        int elapsedTime = Epoch.SecondsElapsed(loginTime, logOffTime);
        totalTime += elapsedTime;

        timeOffline = elapsedTime;
        GameManager.Instance.PlayerEntered(timeOffline);
        Debug.Log(elapsedTime+"seconds");
    }


    void Update()
    {
        playTime += 1 * Time.deltaTime;
        totalTime += Time.deltaTime;
    }

    private void GetTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("playTime", playTime);
        PlayerPrefs.GetFloat("totalTime", totalTime);
        
        logOffTime = (int)System.DateTime.UtcNow.Second;
        PlayerPrefs.SetInt("logOffTime", logOffTime);
    }
}
}
