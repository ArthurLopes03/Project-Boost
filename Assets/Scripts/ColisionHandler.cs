using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    public SO_GameData gameData;
    public SO_SceneManager sceneManager;
    public SO_StatisticalData statsData;

    public AudioSource crashSFX;
    public AudioSource winSFX;

    public ParticleSystem crashPS;
    public ParticleSystem winPS;

    bool isTransitioning = false;

    public float delay = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    isTransitioning = true;
                    Land();
                    winPS.Play();
                    break;
                default:
                    isTransitioning = true;
                    Crash();
                    crashPS.Play();
                    break;
            }
        }
    }

    private void Start()
    {
        isTransitioning = false;
    }

    void Land()
    {
        winSFX.Play();
        Invoke("LoadNextScene", delay);
    }
    void Crash()
    {
        if (gameData.gameStatus.scrap >= 1)
        {
            gameData.gameStatus.scrap--;
            GetComponent<Movement>().audioSource.Stop();
            crashSFX.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadScene", delay);
            sceneManager.sceneData.newLevel = false;
        }
        else
        {
            sceneManager.sceneData.newLevel = true;
            gameData.gameStatus.scrap--;
            Invoke("LoadLoseScreen", delay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        statsData.stats.levelsCleared++;
        sceneManager.sceneData.newLevel = true;
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            gameData.gameStatus.currentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            gameData.gameStatus.currentLevel = 2;
            SceneManager.LoadScene(2);
        }
    }

    void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose Scene");
    }
}
