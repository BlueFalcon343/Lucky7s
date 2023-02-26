using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public GameObject mainMenu;
   public GameObject credits;
   public GameObject controls;

   int screen = 0;
   
   void Start()
   {
      credits.SetActive(false);
      controls.SetActive(false);
      mainMenu.SetActive(true);
   }
   void Update()
   {
      if (Input.GetButtonDown("PlayGame") && (screen == 0))
      {
         screen = 3;
         PlayGame();
         Check();
      }
      else if (Input.GetButtonDown("PlayGame") && (screen == 1))
      {
         OffCredits();
         OffControls();
         Check();
      }

      if (Input.GetButtonDown("QuitGame") && (screen == 0))
      {
         QuitGame();
         Check();
      }
      else if (Input.GetButtonDown("QuitGame") && (screen == 1))
      {
         OffCredits();
         OffControls();
         Check();
      }

      if (Input.GetButtonDown("Credits") && (screen == 0))
      {
         OnCredits();
         Check();
      }
      else if (Input.GetButtonDown("Credits") && (screen == 1))
      {
         OffCredits();
         OffControls();
         Check();
      }

      if (Input.GetButtonDown("Controls") && (screen == 0))
      {
         OnControls();
         Check();
      }
      else if (Input.GetButtonDown("Controls") && (screen == 1))
      {
         OffCredits();
         OffControls();
         Check();
      }

   }

   public void PlayGame()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void QuitGame()
   {
    Debug.Log("QUIT");
    Application.Quit();
   }
   void OnCredits()
   {
      credits.SetActive(true);
      mainMenu.SetActive(false);
      screen = 1;
   }
   void OnControls()
   {
      controls.SetActive(true);
      mainMenu.SetActive(false);
      screen = 1;
   }
   void OffCredits()
   {
      mainMenu.SetActive(true);
      credits.SetActive(false);
      screen = 0;
   }
   void OffControls()
   {
      mainMenu.SetActive(true);
      controls.SetActive(false);
      screen = 0;
   }

   void Check()
   {
      Debug.Log(screen);
   }
}