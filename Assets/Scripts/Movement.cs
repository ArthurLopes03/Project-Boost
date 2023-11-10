using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    
    
    public AudioSource audioSource;
    public AudioClip mainEngine;
    public float thrust = 10F;
    public float rotationSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }

        else { audioSource.Stop();}
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotationSpeed);
        }
    }

    private void Rotate(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}