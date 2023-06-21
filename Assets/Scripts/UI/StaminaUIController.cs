using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private float maxStamina;
    [SerializeField] Player player;
    [SerializeField] Gradient gradient;
    [SerializeField] Image image;
    void Start()
    {
        image.color = gradient.Evaluate(0);
        maxStamina = player.GetMaxStamina();
        slider = GetComponent<Slider>();
        slider.value = maxStamina;
        slider.maxValue = maxStamina;
    }
    public void SetPlayerStamina(float stamina){
        slider.value = stamina;
    }
}
