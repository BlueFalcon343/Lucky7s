using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
   void Update()
   {
      if(Input.GetButtonDown("QuitGame"))
      {
         Debug.Log("Press");
         QuitGame();
      }
      if(Input.GetButtonDown("PlayGame"))
      {
         LoadMenu();
      }
   }
   
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
