using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 10f; // Additional speed when sprinting
    [SerializeField] private float stamina = 10f; // Stamina for sprinting
    [SerializeField] private float staminaReduceRate = 10f; // Stamina recovery rate per second

    [SerializeField] private float staminaRecoveryRate = 2f; // Stamina recovery rate per second


    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private EndGameCamera endGameCamera;
    [SerializeField] private GameObject Fish1;
    [SerializeField] private GameObject Fish2;
    [SerializeField] private GameObject Fish3;
    private Fish currentFish;
    private const float baseAnimationSpeed = 1.8f;

    private CharacterController characterController; // Reference to the CharacterController component
    private UnderWaterScript underWaterScript; // Reference to the UnderWaterScript component
    private bool isSprinting = false; // Flag to track sprinting state
    private float currentStamina; // Current stamina level
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        underWaterScript = this.GetComponentInChildren<UnderWaterScript>();

        Fish1.SetActive(true);
        currentFish = Fish1.GetComponent<Fish>();
        this.currentFish.onLevelUp += GrowUp;
        currentStamina = stamina; // Initialize stamina to maximum value

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

            float animationSpeedMultiplier = isSprinting ? sprintSpeedMultiplier : baseAnimationSpeed;
            currentFish.SetAnimationSpeed(animationSpeedMultiplier);

            inputVector *= speed * animationSpeedMultiplier;
        }
        else
        {
            isSprinting = false;
            currentFish.SetAnimationSpeed(baseAnimationSpeed);
        }

        currentFish.SetIsSwimming(inputVector.magnitude > 0);

        keepPlayerInPool();

        Vector3 movement = inputVector * Time.deltaTime;
        characterController.Move(movement);

        // Stamina mechanics
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            // Must have at least 10% stamina to sprint
            isSprinting = currentStamina > 0.1f * stamina ? true : false;
            currentStamina -= Time.deltaTime * staminaReduceRate;
        }
        else
        {
            isSprinting = false;
            currentStamina = Mathf.Min(currentStamina + (staminaRecoveryRate * Time.deltaTime), stamina);
        }
    }

    private void keepPlayerInPool()
    {
        float waterHeight = this.underWaterScript.GetWaterHeight();
        float waterInnerRadius = 0.5f * this.underWaterScript.GetWaterInnerRadius();
        Vector3 waterCenter = this.underWaterScript.GetWaterCenterPoint();
        Vector3 playerPosition = transform.position;

        // Prevent the player from jumping out of the water surface
        if (playerPosition.y > waterHeight)
        {
            playerPosition.y = waterHeight;
        }

        // Prevent the player from going outside the inner radius
        float distanceFromCenter = Vector3.Distance(playerPosition, waterCenter);
        if (distanceFromCenter > waterInnerRadius)
        {
            Vector3 directionFromCenter = (playerPosition - waterCenter).normalized;
            playerPosition = waterCenter + directionFromCenter * waterInnerRadius;
        }

        characterController.Move(playerPosition - transform.position);
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
