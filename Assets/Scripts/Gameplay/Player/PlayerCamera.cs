using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    [HideInInspector] public Vector3 cameraDirection = Vector3.zero;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= inputY;

        transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
        player.Rotate(Vector3.up * inputX);
        player.Rotate(Vector3.left * inputY);

        // Lock Z rotation
        player.localEulerAngles = new Vector3(player.localEulerAngles.x, player.localEulerAngles.y, 0f);

        // Calculate camera direction relative to player eye position
        Vector3 playerEyePosition = player.position + Vector3.up; // Adjust the height if needed
        cameraDirection = (playerEyePosition - cameraTransform.position).normalized;
    }

}
