using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapScript : MonoBehaviour
{
    public int elementIndex;
    public SO_SceneManager sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Movement>().gameData.gameStatus.scrap++;

            Destroy(this.gameObject);

            sceneManager.scrapDict.Remove(elementIndex);
        }
    }
}
