using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] PlayerCamera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerCamera.cameraDirection);
        Vector3 inputVector = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W)){
            inputVector+= playerCamera.cameraDirection;
        }
        inputVector=inputVector.normalized;
        transform.position+=inputVector*Time.deltaTime*speed;
    }
}
