using Packages.Rider.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform playerPosition;
    private Vector3 newPosition;
    private Rigidbody rb;
    private bool isJumping = false;

    [Header("Player Attributes")]
    public float thrust = 2f;
    public float turnSpeed = 4f;
    public float jumpHeight = 5f;

    private bool reset;
    private bool firstButtonPressed;
    private float timeOfFirstButton;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(transform.forward * thrust);
            rb.AddForce(0, 0, thrust, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-thrust, 0, 0, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(turnSpeed, 0, 0, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -turnSpeed, ForceMode.VelocityChange);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rb.AddForce(0, jumpHeight, 0, ForceMode.VelocityChange);
                isJumping = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void Update()
    {
        /*
        newPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.S))
        {
            newPosition.z -= 1;
            transform.position = newPosition;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            newPosition.x -= 1;
            transform.position = newPosition;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            newPosition.x += 1;
            transform.position = newPosition;
        }

        if (Input.GetKeyDown(KeyCode.W) && firstButtonPressed)
        {
            if (Time.time - timeOfFirstButton < 0.5f)
            {
                Debug.Log("DoubleClicked");
                newPosition.z += 3;
                transform.position = newPosition;
            }
            else
            {
                Debug.Log("Too late");
            }

            reset = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && !firstButtonPressed)
        {
            newPosition.z += 1;
            transform.position = newPosition;

            firstButtonPressed = true;
            timeOfFirstButton = Time.time;
        }

        if (reset)
        {
            firstButtonPressed = false;
            reset = false;
        }
        */
    }
}
