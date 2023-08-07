using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
