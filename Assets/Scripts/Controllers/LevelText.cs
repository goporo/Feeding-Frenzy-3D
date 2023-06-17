using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] Text levelText;
    [SerializeField] Player player;
    private void Update()
    {
        levelText.text = player.GetLevel() + "";
    }
}
