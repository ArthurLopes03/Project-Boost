using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Untagged")
        {
            Destroy(other.gameObject);
        }
    }
}
