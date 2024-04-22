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
        //On start, fuel orb adjusts size based on fuel amount
        fuelPos = GetComponent<Transform>();
        Vector3 scale;
        scale.y = fuel/2 + 1;
        scale.z = fuel/2 + 1;
        scale.x = fuel/2 + 1;
        GetComponent<Transform>().transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if triggerer is player
        if (other.gameObject.tag == "Player" && !isFueling)
        {
            //Adds fuel to player
            other.GetComponent<Movement>().gm.gameStatus.fuel += fuel;
            isFueling = true;
            //Grabs player position
            playerPos = other.gameObject.GetComponent<Transform>();
            //Destroys Orb after 0.3 seconds
            Invoke("DestroyOrb", 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos != null)
        {
            pos = playerPos.position;
            fuelPos.position = Vector3.MoveTowards(fuelPos.position, pos, 0.1f);
            fuelPos.localScale = Vector3.Lerp(fuelPos.localScale, new Vector3(0.1f, 0.1f, 0.1f), shrinkRate * Time.deltaTime);
        }
    }

    private void DestroyOrb()
    {
        this.gameObject.SetActive(false);
    }
}
