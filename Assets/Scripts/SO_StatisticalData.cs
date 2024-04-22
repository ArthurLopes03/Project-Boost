using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct GameStatistics
{
    public float topFuel;
    public int topScrap;
    public int levelsCleared;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StatsData_SO", order = 1)]
public class SO_StatisticalData : ScriptableObject
{
    public GameStatistics stats;

    public void CheckFuel(float fuel)
    {
        if(fuel > stats.topFuel)
            stats.topFuel = fuel;
    }

    public void CheckScrap(int scrap)
    {
        if (scrap > stats.topScrap)
            stats.topScrap = scrap;
    }
}
