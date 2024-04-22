using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System.IO;

public class RecordProcessor : MonoBehaviour
{
    public SO_StatisticalData statsSO;

    public SO_RecordsData recordsSO;

    public SO_GameData gameDataSO;

    const string FILE_NAME = "RecordsDataSave.json";
    string filePath;


    private void Start()
    {
        CompareRecords();
    }
    void CompareRecords()
    {
        if(statsSO.stats.topFuel > recordsSO.records.topFuel)
        {
            recordsSO.records.topFuel = statsSO.stats.topFuel;
            recordsSO.records.topFuelHolder = gameDataSO.gameStatus.playerName;
        }

        if(statsSO.stats.topScrap > recordsSO.records.topScrap)
        {
            recordsSO.records.topScrap = statsSO.stats.topScrap;
            recordsSO.records.topScrapHolder = gameDataSO.gameStatus.playerName;
        }

        if(statsSO.stats.levelsCleared > recordsSO.records.mostLevels)
        {
            recordsSO.records.mostLevels = statsSO.stats.levelsCleared;
            recordsSO.records.mostLevelsHolder = gameDataSO.gameStatus.playerName;
        }
    }

    private void OnApplicationQuit()
    {
        string gameDataJSON = JsonUtility.ToJson(recordsSO.records);

        filePath = Application.persistentDataPath;

        File.WriteAllText(filePath + "/" + FILE_NAME, gameDataJSON);
    }
}