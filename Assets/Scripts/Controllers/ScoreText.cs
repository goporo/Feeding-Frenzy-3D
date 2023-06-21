using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI score;
    void Start()
    {
        score = GetComponent<TextMeshProUGUI>(); 
    }

    private void Update() {
        score.text = player.GetScore().ToString();
    }
}
