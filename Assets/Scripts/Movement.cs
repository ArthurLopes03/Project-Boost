using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    public float fuel = 100;
    public int fuelBurn = 1;
    public TMP_Text textComponent;

    public AudioSource audioSource;
    public AudioClip mainEngine;
    public float thrust = 10F;
    public float rotationSpeed = 10f;
    public ParticleSystem mainThrust;
    public ParticleSystem leftThrust;
    public ParticleSystem rightThrust;

    public Material mainThrustMat;
    public Material rightThrustMat;
    public Material leftThrustMat;
    // Start is called before the first frame update
    void Start()
    {
        mainThrustMat.color = Color.black;
        rightThrustMat.color = Color.black;
        leftThrustMat.color = Color.black;

        textComponent.text = "Fuel : " + fuel.ToString("F1");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mainThrustMat.color = Color.Lerp(mainThrustMat.color, Color.black, 0.2f * Time.deltaTime);
        rightThrustMat.color = Color.Lerp(rightThrustMat.color, Color.black, 0.2f * Time.deltaTime);
        leftThrustMat.color = Color.Lerp(leftThrustMat.color, Color.black, 0.2f * Time.deltaTime);

        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            mainThrustMat.color = Color.Lerp(mainThrustMat.color, Color.red, 0.5f * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (!mainThrust.isPlaying)
            {
                mainThrust.Play();
            }

            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);

            fuel -= Time.deltaTime * fuelBurn;

            if (fuel < 0)
                fuel = 0;
            textComponent.text = "Fuel : " + fuel.ToString("F1");
        }

        else 
        {
            audioSource.Stop(); mainThrust.Stop(); 
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rightThrustMat.color = Color.Lerp(rightThrustMat.color, Color.red, 0.5f * Time.deltaTime);

            Rotate(rotationSpeed);
            if (!rightThrust.isPlaying)
            {
                rightThrust.Play();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            leftThrustMat.color = Color.Lerp(leftThrustMat.color, Color.red, 0.5f * Time.deltaTime);

            Rotate(-rotationSpeed);
            if (!leftThrust.isPlaying)
            {
                leftThrust.Play();
            }

        }
        else
        {
            leftThrust.Stop();
            rightThrust.Stop();
        }
        
    }

    private void Rotate(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}