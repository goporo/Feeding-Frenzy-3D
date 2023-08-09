using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpText : MonoBehaviour
{
    [SerializeField] Text expText;
    [SerializeField] Player player;
    private void Update()
    {
        expText.text = player.GetExp() + "/" + player.GetMaxExp();
    }
}
