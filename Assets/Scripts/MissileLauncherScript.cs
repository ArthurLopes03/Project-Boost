using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public Object prefab;

    public Transform spawnPoint;

    public bool canSpawn = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSpawn == true)
        {
            canSpawn = false;
            SpawnRocket();
        }
    }

    public void SpawnRocket()
    {
        canSpawn = false;
        Vector3 spawnPos = spawnPoint.position;
        Instantiate(prefab, spawnPos, spawnPoint.rotation);
    }
}
