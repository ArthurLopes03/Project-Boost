using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public SO_SceneManager sceneManager;
    public GameObject fuelOrbPrefab;
    public GameObject scrapPrefab;

    private void Start()
    {
        sceneManager.Start();
        SpawnFuelOrbs();
        SpawnScrap();
    }
    void SpawnFuelOrbs()
    {
        foreach (int i in sceneManager.fuelOrbsDict.Keys)
        {
            GameObject fuelOrbObject = Instantiate(fuelOrbPrefab, sceneManager.fuelOrbsDict[i], Quaternion.identity);

            FuelOrb fuelOrb = fuelOrbObject.GetComponent<FuelOrb>();

            fuelOrb.sceneManager = this.sceneManager;

            fuelOrb.elementIndex = i;
        }
    }

    void SpawnScrap()
    {
        foreach (int i in sceneManager.scrapDict.Keys)
        {
            GameObject scrapObject = Instantiate(scrapPrefab, sceneManager.scrapDict[i], Quaternion.identity);

            ScrapScript scrapScript = scrapObject.GetComponent<ScrapScript>();

            scrapScript.sceneManager = this.sceneManager;

            scrapScript.elementIndex = i;
        }
    }
}
