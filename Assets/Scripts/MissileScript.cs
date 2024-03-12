using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    private bool isFiring = false;

    [SerializeField]
    private Transform forward;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float missileSpeed = 10f;

    [SerializeField]
    private ParticleSystem ps;

    private Vector3 direction;

    private Vector3 mousePos;

    private Quaternion lookRotation;

    private Quaternion newRotation;

    private Vector3 eulerAngleRotation;

    [SerializeField]
    private float rotationSpeed = 10f;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Fire", 0.25f);
    }

    void Update()
    {

        if (isFiring)
        {
            rb.AddRelativeForce(Vector3.forward * missileSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        if(Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (Camera.main.transform.position.z * -2)));

            direction = mousePos - transform.position;

            lookRotation = Quaternion.LookRotation(direction, new Vector3(1,0,0));

            newRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            transform.rotation = Quaternion.Euler(newRotation.eulerAngles.x, newRotation.eulerAngles.y, 0f);

            Debug.Log(Quaternion.Euler(newRotation.eulerAngles.x, newRotation.eulerAngles.y, 0f));
        }
    }

    private void Fire()
    {
        ps.Play();
        isFiring = true;
    }
}
