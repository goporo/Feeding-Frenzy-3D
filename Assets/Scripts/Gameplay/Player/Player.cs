using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float maxHealth = 100f;
    public float maxExp = 0;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float sprintSpeedMultiplier = 4f; // Additional speed when sprinting
    [SerializeField] private float maxStamina = 10f; // Stamina for sprinting
    [SerializeField] private float staminaReduceRate = 10f; // Stamina reduce rate per second
    [SerializeField] private float staminaRecoveryRate = 2f; // Stamina recovery rate per second
    [SerializeField] private float healthReduceRate = 2f; // Health reduce rate per second
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private EndGameCamera endGameCamera;
    [SerializeField] private GameObject Fish1;
    [SerializeField] private GameObject Fish2;
    [SerializeField] private GameObject Fish3;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private AudioClip biteFX;


    private Fish currentFish;
    public float exp = 0;
    private const float baseAnimationSpeed = 1.8f;
    private CharacterController characterController; // Reference to the CharacterController component
    private UnderWaterScript underWaterScript; // Reference to the UnderWaterScript component
    private bool isSprinting = false; // Flag to track sprinting state
    private float currentStamina; // Current stamina level
    private float currentHealth;
    private int level = 1;
    private AudioSource audioSource;

    private void Start()
    {
        Fish1.SetActive(true);
        characterController = GetComponent<CharacterController>();
        underWaterScript = this.GetComponentInChildren<UnderWaterScript>();
        audioSource = GetComponent<AudioSource>();

        currentFish = Fish1.GetComponent<Fish>();
        currentStamina = maxStamina; // Initialize stamina to maximum value
        currentHealth = maxHealth;
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
            isSprinting = currentStamina > 0.1f * maxStamina ? true : false;
            currentStamina -= Time.deltaTime * staminaReduceRate;
        }
        else
        {
            isSprinting = false;
            currentStamina = Mathf.Min(currentStamina + (staminaRecoveryRate * Time.deltaTime), maxStamina);
        }

        // Health mechanics
        currentHealth = currentHealth > 0 ? currentHealth - healthReduceRate * Time.deltaTime : 0;

    }

    private void keepPlayerInPool()
    {
        float waterHeight = this.underWaterScript.GetWaterHeight();
        float waterInnerRadius = 0.5f * this.underWaterScript.GetWaterInnerRadius();
        Vector3 waterCenter = this.underWaterScript.GetWaterCenterPoint();
        Vector3 playerPosition = transform.position;

        // Prevent the player from going out of bounds
        if (playerPosition.y > waterHeight)
        {
            playerPosition.y = waterHeight;
        }

        float distanceFromCenter = Vector3.Distance(playerPosition, waterCenter);
        if (distanceFromCenter > waterInnerRadius)
        {
            Vector3 directionFromCenter = (playerPosition - waterCenter).normalized;
            playerPosition = waterCenter + directionFromCenter * waterInnerRadius;
        }

        // Basic movement
        characterController.Move(playerPosition - transform.position);

    }

    public void GrowUp()
    {
        if (this.level > 5)
        {
            Fish3.SetActive(true);
            currentFish = Fish3.GetComponent<Fish>();
            Fish2.SetActive(false);
        }
        else if (this.level > 3)
        {
            Fish2.SetActive(true);
            currentFish = Fish2.GetComponent<Fish>();
            Fish1.SetActive(false);
        }
    }

    public float getExp()
    {
        return this.exp;
    }

    public float getMaxExp()
    {
        return this.maxExp;
    }

    public float GetScore()
    {
        return this.currentFish.GetScore();
    }

    public EndGameCamera GetEndGameCamera()
    {
        return this.endGameCamera;
    }
    public float getCurrentHealth()
    {
        return this.currentHealth;
    }
    public void setHealth(float modifyValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + modifyValue, 0, maxHealth);
    }

    public void LevelUp()
    {
        ParticleSystem effectInstance = Instantiate(levelUpEffect, transform.position, Quaternion.identity);
        effectInstance.transform.parent = transform;
        float effectDuration = effectInstance.main.duration;

        this.exp = this.exp - this.maxExp;
        this.level += 1;
        this.maxExp = this.level * 2;

        StartCoroutine(WaitToGrow(effectDuration));
    }

    private IEnumerator WaitToGrow(float duration)
    {
        yield return new WaitForSeconds(duration);
        GrowUp();
    }
    public int GetLevel()
    {
        return this.level;
    }
    public void SetLevel(int level)
    {
        this.level = level;
    }
    public void PlayBiteSound()
    {
        audioSource.clip = biteFX;
        audioSource.Play();
    }


}
