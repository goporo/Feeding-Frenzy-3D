using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 10f; // Additional speed when sprinting
    [SerializeField] private float stamina = 10f; // Stamina for sprinting

    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private EndGameCamera endGameCamera;
    [SerializeField] private GameObject Fish1;
    [SerializeField] private GameObject Fish2;
    [SerializeField] private GameObject Fish3;
    private Fish currentFish;
    private const float baseAnimationSpeed = 1.8f;

    private CharacterController characterController; // Reference to the CharacterController component

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Fish1.SetActive(true);
        currentFish = Fish1.GetComponent<Fish>();
        this.currentFish.onLevelUp += GrowUp;
    }

    private void Update()
    {
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
                inputVector += playerCamera.cameraDirection;
            else
                inputVector -= playerCamera.cameraDirection;

            // Check if the Shift key is held down to sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentFish.SetAnimationSpeed(sprintSpeedMultiplier);
                inputVector *= speed * sprintSpeedMultiplier;
            }
            else
            {
                currentFish.SetAnimationSpeed(baseAnimationSpeed);
                inputVector *= speed;
            }
        }
        // fish idle 
        else
            currentFish.SetAnimationSpeed(baseAnimationSpeed);

        currentFish.SetIsSwimming(inputVector.magnitude > 0);

        // Prevent the player from jumping out of the water surface
        float waterHeight = this.GetComponentInChildren<UnderWaterScript>().GetWaterHeight();
        if (transform.position.y > waterHeight)
        {
            Vector3 newPosition = new Vector3(transform.position.x, waterHeight, transform.position.z);
            characterController.Move(newPosition - transform.position);
        }

        // Apply player movement using the CharacterController
        characterController.Move(inputVector * Time.deltaTime);
    }

    private void GrowUp(object sender, EventArgs e)
    {
        Debug.Log("Lvl cua ca: " + currentFish.GetLevel());
        if (currentFish.GetLevel() > 5)
        {
            Fish3.SetActive(true);
            currentFish = Fish3.GetComponent<Fish>();
            Fish2.SetActive(false);
        }
        else if (currentFish.GetLevel() > 3)
        {
            Fish2.SetActive(true);
            currentFish = Fish2.GetComponent<Fish>();
            OnGrowUpAction();
            Fish1.SetActive(false);
        }
    }

    private void OnGrowUpAction()
    {
        this.currentFish.onLevelUp += GrowUp;
    }

    public float getExp()
    {
        return this.currentFish.GetExp();
    }

    public float getLevel()
    {
        return this.currentFish.GetLevel();
    }

    public float getMaxExp()
    {
        return this.currentFish.GetMaxExp();
    }

    public float GetScore()
    {
        return this.currentFish.GetScore();
    }

    public EndGameCamera GetEndGameCamera()
    {
        return this.endGameCamera;
    }
}
