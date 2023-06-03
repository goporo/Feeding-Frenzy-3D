using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Player player;
    private void Update() {
        scoreText.text = player.GetScore()+"";
    }
}
