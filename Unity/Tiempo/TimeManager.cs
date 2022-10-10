using UnityEngine;
using System;

namespace PlayTime{

public class TimeManager : MonoBehaviour
{
    public float playTime;
    public float totalTime;
    int logOff_year;
    int logOff_month;
    int logOff_day;
    int logOff_hour;
    int logOff_minute;
    int logOff_second;


    public static int OfflineSeconds {get; private set;}
    public static int OfflineMinutes {get; private set;}

    public delegate void IntVoidDelegate(int amount);
    public static event IntVoidDelegate OnMinuteTick;
    public static event IntVoidDelegate OnTimerLoaded;
    public static event IntVoidDelegate OnTimeCleared;


    public void CleanTimeData()
    {
        PlayerPrefs.SetFloat("playTime", 0);
        PlayerPrefs.SetFloat("totalTime", 0);
        playTime = 0;
        totalTime = 0;
        OfflineSeconds = 0;
        OfflineMinutes = 0;
    }

    public void CalculateOfflineTime()
    {
        playTime = PlayerPrefs.GetFloat("playTime");
        playTime = 0;
        totalTime = PlayerPrefs.GetFloat("totalTime");

        logOff_year = PlayerPrefs.GetInt("logOff_Year");
        logOff_month= PlayerPrefs.GetInt("logOff_Month");
        logOff_day = PlayerPrefs.GetInt("logOff_Day");
        logOff_hour = PlayerPrefs.GetInt("logOff_Hour");
        logOff_minute = PlayerPrefs.GetInt("logOff_Minute");
        logOff_second = PlayerPrefs.GetInt("logOff_Second");

        System.DateTime t1 = new System.DateTime(logOff_year, logOff_month, logOff_day, logOff_hour, logOff_minute, logOff_second);
        System.DateTime t2  = System.DateTime.UtcNow; 

        double elapsedHours = (t2 - t1).TotalHours;
        double elapsedMinutes = (t2 - t1).TotalMinutes;
        double elapsedSeconds = (t2 - t1).TotalSeconds;

        int elapsedTime = (int)elapsedSeconds;
        totalTime += elapsedTime;

        OfflineSeconds = elapsedTime;
        OfflineMinutes = Mathf.FloorToInt(elapsedTime/60);
        Debug.Log("Offline for "+ OfflineMinutes + " min.");

        if(OnTimerLoaded != null) 
            OnTimerLoaded(OfflineSeconds);
    }

    void Update()
    {
        playTime += 1 * Time.deltaTime;
        totalTime += 1 * Time.deltaTime;

        if((int)playTime % 60 == 0)
        {
            if(OnMinuteTick != null)
                OnMinuteTick( Mathf.FloorToInt(playTime / 60) );
        }
    }

    private void GetTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("App quit");
        PlayerPrefs.SetFloat("playTime", playTime);
        PlayerPrefs.SetFloat("totalTime", totalTime);
        
        PlayerPrefs.SetInt("logOff_Year", System.DateTime.UtcNow.Year);
        PlayerPrefs.SetInt("logOff_Month", System.DateTime.UtcNow.Month);
        PlayerPrefs.SetInt("logOff_Day", System.DateTime.UtcNow.Day);
        PlayerPrefs.SetInt("logOff_Hour", System.DateTime.UtcNow.Hour);
        PlayerPrefs.SetInt("logOff_Minute", System.DateTime.UtcNow.Minute);
        PlayerPrefs.SetInt("logOff_Second", System.DateTime.UtcNow.Second);
    }
}
}
