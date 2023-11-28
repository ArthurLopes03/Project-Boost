using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
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
        GetComponent<Movement>().audioSource.Stop();
        crashSFX.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
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
