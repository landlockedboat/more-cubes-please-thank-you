using UnityEngine;
using System.Collections.Generic;

public class StatisticsControl : MonoBehaviour {

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
                    Debug.LogError("There needs to be one active StatisticsControl script on a GameObject in your scene.");
                }
                else
                {
                    statisticsControl.Init();
                }
            }

            return statisticsControl;
        }
    }

    void Init()
    {
        stats = new Dictionary<Stat, int>();
    }

    public static void SetStat(Stat s, int value)
    {
        if (instance.stats.ContainsKey(s))
            instance.stats[s] = value;
        else
            instance.stats.Add(s, value);
    }

    public static int GetStat(Stat s) {
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
        totalEnemiesKilled, enemiesKilledByBullets, bulletsShot, missilesShot, maxMultiplierHad, accuracy, healedLife,
        enemiesKilledByMissile
    }
}
