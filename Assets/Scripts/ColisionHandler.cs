using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    public AudioSource crashSFX;
    public AudioSource winSFX;

    bool isCrashing = false;
    bool isWinning = false;

    public float delay = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                Land();
                break;
            default:
                Crash();
                break;
        }
    }


    void Land()
    {
        if (!isWinning)
        {
            isWinning = true;
            winSFX.Play();
            Invoke("LoadNextScene", delay);
        }
    }
    void Crash()
    {
        if (!isCrashing)
        {
            GetComponent<Movement>().audioSource.Stop();
            isCrashing = true;
            crashSFX.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadScene", delay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }
}
