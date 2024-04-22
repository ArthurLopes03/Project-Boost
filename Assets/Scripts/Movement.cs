using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    public SO_GameData gm;

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
        gm.Start();

        //Thruster Mat is set to black on start
        mainThrustMat.color = Color.black;
        rightThrustMat.color = Color.black;
        leftThrustMat.color = Color.black;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Thrusters will return to black over time
        mainThrustMat.color = Color.Lerp(mainThrustMat.color, Color.black, 0.2f * Time.deltaTime);
        rightThrustMat.color = Color.Lerp(rightThrustMat.color, Color.black, 0.2f * Time.deltaTime);
        leftThrustMat.color = Color.Lerp(leftThrustMat.color, Color.black, 0.2f * Time.deltaTime);

        textComponent.text = gm.UpdateStatus();

        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && gm.gameStatus.fuel > 0)
        {
            //When the thruster is active, the colour of the mat is lerped between black and red over time
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

            gm.gameStatus.fuel -= Time.deltaTime * fuelBurn;

            if (gm.gameStatus.fuel < 0)
                gm.gameStatus.fuel = 0;
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