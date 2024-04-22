using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameStatus
{
    public float fuel;
	public string playerName;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager_SO", order = 1)]
public class SO_GameData : ScriptableObject
{
    public GameStatus gameStatus;

	public void Start()
    {
		LoadGameStatus();
    }

	public void LoadGameStatus()
	{
		// Check for previous play or death!
		if (gameStatus.playerName == null)
		{
			// If new game, create new struct
			gameStatus = new GameStatus();
			//initilise a new game status
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
		gameStatus.fuel = 10;
		gameStatus.playerName = "Arthur";
    }

	public string UpdateStatus()
    {
		string message = "";
		message += "Fuel: " + Mathf.Round(gameStatus.fuel);

		return message;
    }
}
