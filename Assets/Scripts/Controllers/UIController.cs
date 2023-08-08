using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image fish3Icon;
    [SerializeField] private Image fish4Icon;
    [SerializeField] private ExpUIController expUIController;
    [SerializeField] private HealthUIController healthUIController;
    [SerializeField] private StaminaUIController staminaUIController;
    [SerializeField] private PlayerLevelUIController playerLevelUIController;
    [SerializeField] private Image bloodSplatterImage;
    [SerializeField] private Image bloodRadialImage;
    [SerializeField] private GameObject pauseMenu;
    [HideInInspector] public bool isDead = false;
    bool isPaused = false;

    public void GrowUp1()
    {
        fish3Icon.gameObject.SetActive(true);
    }
    public void GrowUp2()
    {
        fish4Icon.gameObject.SetActive(true);

    }
    public void UpdateHealth(float currentHealth)
    {
        healthUIController.SetPlayerHP(currentHealth);

    }
    public void UpdateStamina(float currentStamina)
    {
        staminaUIController.SetPlayerStamina(currentStamina);

    }

    public void UpdateLevel(int level)
    {
        playerLevelUIController.SetText(level);

    }

    public ExpUIController GetExpUIController()
    {
        return expUIController;
    }

    public IEnumerator HurtFlash(float hurtTimer)
    {
        bloodRadialImage.enabled = true;
        bloodSplatterImage.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        bloodRadialImage.enabled = false;

        bloodSplatterImage.enabled = false;
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);

    }

    public void ShowMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
        {
            if (!isPaused)
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
            isPaused = !isPaused;
        }
    }
}
