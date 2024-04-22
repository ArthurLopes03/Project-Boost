using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Records
{
    public float topFuel;
    public string topFuelHolder;

    public int topScrap;
    public string topScrapHolder;

    public int mostLevels;
    public string mostLevelsHolder;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RecordsData_SO", order = 1)]
public class SO_RecordsData : ScriptableObject
{
    public Records records;

    public string ReturnRecord()
    {
        string record = "";

        record += "Most Fuel Collected: " + records.topFuelHolder + " " + records.topFuel;
        record += "\nMost Scrap Collected: " + records.topScrapHolder + " " + records.topScrap;
        record += "\nMost Levels Cleared: " + records.mostLevelsHolder + " " + records.mostLevels;

        return record;
    }
}
