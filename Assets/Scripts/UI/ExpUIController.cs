using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private float maxExp = 200;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = maxExp / 2;
        slider.maxValue = maxExp;
    }
    public void OnPlayerEating(float exp)
    {
        slider.value += exp;
        if (slider.value > maxExp)
        {
            slider.value = maxExp;
        }
    }
}
