using UnityEngine;
using System.Collections.Generic;

public class StatisticsControl : MonoBehaviour
{

    Dictionary<Stat, int> stats;

    static StatisticsControl statisticsControl;

    public static StatisticsControl instance
    {
        get
        {
            if (!statisticsControl)
            {
                statisticsControl = FindObjectOfType<StatisticsControl>();
                if (!statisticsControl)
                {                    
                    GameObject go = new GameObject();
                    go.name = "AudioSettingsControl";
                    go.AddComponent<StatisticsControl>();
                    statisticsControl = FindObjectOfType<StatisticsControl>();
                    Debug.Log("StatisticsControl script created on a GameObject in your scene.");
                }
                statisticsControl.Init();

            }

            return statisticsControl;
        }
    }

    void Init()
    {
        stats = new Dictionary<Stat, int>();
        DontDestroyOnLoad(gameObject);
    }

    public static void SetStat(Stat s, int value)
    {
        if (instance.stats.ContainsKey(s))
            instance.stats[s] = value;
        else
            instance.stats.Add(s, value);
    }

    public static int GetStat(Stat s)
    {
        int val = -1;
        instance.stats.TryGetValue(s, out val);
        return val;
    }

    public static void AddToStat(Stat s, int delta)
    {
        if (instance.stats.ContainsKey(s))
            instance.stats[s] += delta;
        else
            instance.stats.Add(s, delta);
    }

    public enum Stat
    {
        TotalEnemiesKilled, EnemiesKilledByBullets, BulletsShot, MissilesShot, MaxMultiplierHad, Accuracy, HealedLife,
        EnemiesKilledByMissile
    }
}
