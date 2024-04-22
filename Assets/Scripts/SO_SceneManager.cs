using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct SceneData
{
    public List<Vector3> fuelOrbs;
    public List<Vector3> scrap;
    public bool newLevel;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelData_SO", order = 1)]
public class SO_SceneManager : SerializedScriptableObject
{
    public Dictionary<int, Vector3> fuelOrbsDict;

    public Dictionary<int, Vector3> scrapDict;

    public SceneData sceneData;

    public void Start()
    {
        if(sceneData.newLevel)
        {
            CreateDictionaries();
        }
    }

    void CreateDictionaries()
    {
        fuelOrbsDict = new Dictionary<int, Vector3> { };

        int index = 0;
        foreach (Vector3 i in sceneData.fuelOrbs)
        {
            fuelOrbsDict.Add(index, i);
            index++;
        }

        scrapDict = new Dictionary<int, Vector3> { };

        index = 0;
        foreach (Vector3 i in sceneData.scrap)
        {
            scrapDict.Add(index, i);
            index++;
        }
    }
}