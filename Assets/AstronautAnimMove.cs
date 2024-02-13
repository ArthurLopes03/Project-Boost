using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautAnimMove : MonoBehaviour
{
    Transform transform;
    Animator animator;
    float horizInput;
    bool lookingLeft;

    void Start()
    {
        //get the Animator Controller Component from the character component hierarchy
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        //get the input from the vertical axis
        horizInput = Input.GetAxis("Horizontal");

        if( horizInput > 0 && lookingLeft == true)
        {
            lookingLeft = false;
            transform.Rotate(0, 180, 0);
        }
        else if(horizInput < 0 && lookingLeft == false)
        {
            lookingLeft = true;
            transform.Rotate(0, -180, 0);
        }
    }
    void FixedUpdate()
    {
        //use fixed update to control the animation as it will behave in real time
        //now set the animator float value (vAxisInput) with the input value
        animator.SetFloat("hAxisInput", horizInput);
    }

}
