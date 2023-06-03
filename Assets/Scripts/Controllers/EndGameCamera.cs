using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCamera : MonoBehaviour
{
    public void OpenEndGameMenu(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.gameObject.SetActive(true);
    }
    public void RestartGame(){
        SceneManager.LoadScene("MainScene");
        Time.timeScale=1;
    }
}
