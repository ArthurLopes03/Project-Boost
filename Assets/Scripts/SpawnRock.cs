using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{
    private float timer = 1f;
    public float timeToSpawn = 3f;
    public GameObject debris;
    private Vector3 pos;
    void Start()
    {
        pos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            debris = Instantiate(debris, pos, transform.rotation) as GameObject;
            timer = timeToSpawn;
        }
    }
}
