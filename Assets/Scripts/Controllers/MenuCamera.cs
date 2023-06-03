using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject fishSpawner;
    [SerializeField] GameObject menuCamera;
    public void OnPlayButtonPressed(){
        player.SetActive(true);
        fishSpawner.SetActive(true);
        menuCamera.SetActive(false);
        // Destroy(menuCamera);
    }
}
