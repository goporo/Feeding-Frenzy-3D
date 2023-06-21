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

    // Adjust the smoothing factor as per your preference
    public float mouseSmoothing = 0f;
    // The value should be between 0 and 1, where 0 means no smoothing and 1 means maximum smoothing.
    private Vector2 smoothMouseInput;

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        // Apply smoothing to the mouse input
        smoothMouseInput.x = Mathf.Lerp(smoothMouseInput.x, inputX, mouseSmoothing);
        smoothMouseInput.y = Mathf.Lerp(smoothMouseInput.y, inputY, mouseSmoothing);

        float smoothInputX = smoothMouseInput.x * mouseSensitivity * Time.deltaTime;
        float smoothInputY = smoothMouseInput.y * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= smoothInputY;

        transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
        player.Rotate(Vector3.up * smoothInputX);
        player.Rotate(Vector3.left * smoothInputY);

        // Lock Z rotation
        player.localEulerAngles = new Vector3(player.localEulerAngles.x, player.localEulerAngles.y, 0f);

        // Calculate camera direction relative to player eye position
        Vector3 playerEyePosition = player.position + Vector3.up; // Adjust the height if needed
        cameraDirection = (playerEyePosition - cameraTransform.position).normalized;
    }

}
