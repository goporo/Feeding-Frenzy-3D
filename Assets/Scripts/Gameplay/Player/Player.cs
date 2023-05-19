using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 8f; // Additional speed when sprinting
    [SerializeField] private PlayerCamera playerCamera;

    private bool isSprinting = false; // Flag to track if the player is sprinting

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector += playerCamera.cameraDirection;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVector -= playerCamera.cameraDirection;
        }

        inputVector = inputVector.normalized;

        // Check if the Shift key is held down to sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            inputVector *= speed * sprintSpeedMultiplier;
        }
        else
        {
            isSprinting = false;
            inputVector *= speed;
        }

        transform.position += inputVector * Time.deltaTime;
    }
}
