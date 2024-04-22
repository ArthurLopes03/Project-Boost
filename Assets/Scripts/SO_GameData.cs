using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public struct GameStatus
{
	public int scrap;
    public float fuel;
	public string playerName;

	public int currentLevel;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData_SO", order = 1)]
public class SO_GameData : ScriptableObject
{
    public GameStatus gameStatus;

    const string FILE_NAME = "GameDataSaveStatus.json";

    string filePath;

    public void Start()
    {
        string filePath = Application.persistentDataPath;

        LoadGameStatus();
    }

	public void LoadGameStatus()
	{

        if (gameStatus.playerName == null || gameStatus.scrap < 0)
		{
			gameStatus = new GameStatus();
			ResetGame();
			Debug.Log("File not found");
		}
		else
		{
			// Do nothing
		}
	}

	public void ResetGame()
    {
		gameStatus.scrap = 1;
		gameStatus.fuel = 10;
		gameStatus.playerName = "Arthur";
		gameStatus.currentLevel = 2;

		SaveGame();
    }

    public void ResetGame(string input)
    {
        gameStatus.scrap = 1;
        gameStatus.fuel = 10;
        gameStatus.playerName = input;
        gameStatus.currentLevel = 2;

        SaveGame();
    }

    public string UpdateStatus()
    {
		string message = "";
		message += "Scrap: " + gameStatus.scrap;
		message += "\nFuel: " + Mathf.Round(gameStatus.fuel);
		return message;
    }

	public void SaveGame()
	{
		string gameDataJSON = JsonUtility.ToJson(gameStatus);

		filePath = Application.persistentDataPath;

        File.WriteAllText(filePath + "/" + FILE_NAME, gameDataJSON);
	}

	public void LoadGame()
	{
        string filePath = Application.persistentDataPath;

        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            //load the file content as string
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            //deserialise the loaded string into a GameStatus struct
            gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            //initilise a new game status
            ResetGame();
            Debug.Log("File not found");
        }
    }
}
