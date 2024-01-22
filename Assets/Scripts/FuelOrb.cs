using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelOrb : MonoBehaviour
{
    public int fuel;
    public float shrinkRate;
    private bool isFueling = false;
    private Transform playerPos = null;
    private Transform fuelPos;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        fuelPos = GetComponent<Transform>();
        Vector3 scale;
        scale.y = fuel;
        scale.z = fuel;
        scale.x = fuel;
        GetComponent<Transform>().transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isFueling)
        {
            other.GetComponent<Movement>().fuel += fuel;
            isFueling = true;
            playerPos = other.gameObject.GetComponent<Transform>();
            Invoke("DestroyOrb", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos != null)
        {
            pos = playerPos.position;
            fuelPos.position = Vector3.MoveTowards(fuelPos.position, pos, 0.02f);
            fuelPos.localScale = Vector3.Lerp(fuelPos.localScale, new Vector3(0.1f, 0.1f, 0.1f), shrinkRate * Time.deltaTime);
        }
    }

    private void DestroyOrb()
    {
        this.gameObject.SetActive(false);
    }
}
