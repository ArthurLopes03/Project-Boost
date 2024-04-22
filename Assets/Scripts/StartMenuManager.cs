using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    public SO_RecordsData records;
    public SO_GameData gameData;
    public GameObject canvas;

    public TMP_Text text;

    const string FILE_NAME = "RecordsDataSave.json";
    string filePath;

    private void Start()
    {
        LoadRecords();
        SetRecordsUI();
    }

    public void StartNewRun(string input)
    {
        Debug.Log(input);

        gameData.ResetGame(input);

        SceneManager.LoadScene(2);
    }

    public void LoadRun()
    {
        gameData.LoadGameStatus();
        SceneManager.LoadScene(gameData.gameStatus.currentLevel);
    }

    public void OpenMenu()
    {
        canvas.SetActive(true);
    }

    void LoadRecords()
    {
        filePath = Application.persistentDataPath;

        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);

            records.records = JsonUtility.FromJson<Records>(loadedJson);
        }
        else
        {
            //Do Nothing
        }
    }

    void SetRecordsUI()
    {
        text.text = records.ReturnRecord();
    }
}
