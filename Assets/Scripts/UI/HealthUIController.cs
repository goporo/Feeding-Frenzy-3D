using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private float maxHp = 100;
    [SerializeField] Player player;
    [SerializeField] Gradient gradient;
    [SerializeField] Image image;
    void Start()
    {
        image.color = gradient.Evaluate(maxHp);
        maxHp = player.GetMaxHealth();
        slider = GetComponent<Slider>();
        slider.value = maxHp;
        slider.maxValue = maxHp;
    }
    public void SetPlayerHP(float hp){
        slider.value = hp;
    }
}
