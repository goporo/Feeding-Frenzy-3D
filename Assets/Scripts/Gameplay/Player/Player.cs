using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }
    private float maxHealth = 100f;
    private float speed = 28f;
    private float sprintSpeedMultiplier = 4f;
    private float maxStamina = 20f;
    private float staminaReduceRate = 10f;
    private float staminaRecoveryRate = 2.5f;
    private float healthReduceRate = 3f;
    private GameObject Fish1;
    private GameObject Fish2;
    private GameObject Fish3;
    [SerializeField] UIController uiController;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private AudioClip biteFX;

    [HideInInspector] public float maxExp = 2;
    [HideInInspector] public float exp = 0;
    [HideInInspector] public float score = 0;

    private Fish currentFish;
    private const float baseAnimationSpeed = 1.8f;
    private CharacterController characterController;
    private UnderWaterScript underWaterScript;
    private bool isSprinting = false;
    private float currentStamina;
    private float currentHealth;
    private int level = 1;
    private AudioSource audioSource;
    private float hurtTimer = 0.2f;

    private void Start()
    {
        Fish1 = transform.GetChild(0).gameObject;
        Fish2 = transform.GetChild(1).gameObject;
        Fish3 = transform.GetChild(2).gameObject;

        Fish1.SetActive(true);
        characterController = GetComponent<CharacterController>();
        underWaterScript = GetComponentInChildren<UnderWaterScript>();
        audioSource = GetComponent<AudioSource>();

        currentFish = Fish1.GetComponent<Fish>();
        currentStamina = maxStamina;
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

        KeepPlayerInPool();

        Vector3 movement = inputVector * Time.deltaTime;
        characterController.Move(movement);

        // Stamina mechanics
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            // Must have at least 10% stamina to sprint
            isSprinting = currentStamina > 0.1f * maxStamina;
            currentStamina -= Time.deltaTime * staminaReduceRate;
        }
        else
        {
            isSprinting = false;
            currentStamina = Mathf.Min(currentStamina + (staminaRecoveryRate * Time.deltaTime), maxStamina);
        }

        // Health mechanics
        if (currentHealth > 0)
        {
            currentHealth -= healthReduceRate * Time.deltaTime;
        }
        else if (!uiController.isDead)
        {
            currentHealth = 0;
            uiController.isDead = true;
            uiController.ShowMenu();

        }

        uiController.UpdateHealth(currentHealth);
        uiController.UpdateStamina(currentStamina);

    }

    private void KeepPlayerInPool()
    {
        float waterHeight = this.underWaterScript.GetWaterHeight();
        float waterInnerRadius = 0.5f * this.underWaterScript.GetWaterInnerRadius();
        Vector3 waterCenter = this.underWaterScript.GetWaterCenterPoint();
        Vector3 playerPosition = transform.position;

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

        characterController.Move(playerPosition - transform.position);

    }

    public void GrowUp()
    {
        if (level == 7)
        {
            Fish3.SetActive(true);
            currentFish = Fish3.GetComponent<Fish>();
            Fish2.SetActive(false);
            uiController.GrowUp2();
            healthReduceRate = 5f;

        }
        else if (level == 5)
        {
            Fish2.SetActive(true);
            currentFish = Fish2.GetComponent<Fish>();
            Fish1.SetActive(false);
            uiController.GrowUp1();
            healthReduceRate = 4f;
        }
    }

    public float GetExp()
    {
        return exp;
    }

    public float GetMaxExp()
    {
        return maxExp;
    }

    public float GetScore()
    {
        return score;
    }

    // public EndGameCamera GetEndGameCamera()
    // {
    //     return endGameCamera;
    // }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void SetHealth(float modifyValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + modifyValue, 0, maxHealth);

        // Take damage
        if (modifyValue < 0 && currentHealth > 0)
        {
            StartCoroutine(uiController.HurtFlash(hurtTimer));
        }
    }


    public void LevelUp()
    {
        uiController.UpdateLevel(level);
        ParticleSystem effectInstance = Instantiate(levelUpEffect, transform.position, Quaternion.identity);
        effectInstance.transform.parent = transform;
        float effectDuration = effectInstance.main.duration;

        exp -= maxExp;
        level += 1;
        maxExp = level * 2;

        StartCoroutine(WaitToGrow(effectDuration));
    }

    private IEnumerator WaitToGrow(float duration)
    {
        yield return new WaitForSeconds(duration);
        GrowUp();
    }
    public int GetLevel()
    {
        return level;
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
    public ExpUIController GetExpUIController()
    {
        return uiController.GetExpUIController();
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetMaxStamina()
    {
        return maxStamina;
    }
}
