using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
   public void LoadMenu()
   {
    SceneManager.LoadScene("Main Menu");
   }
   public void QuitGame()
   {
    Debug.Log("QUIT");
    Application.Quit();
   }
}
