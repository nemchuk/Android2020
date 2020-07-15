using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject MenuNavigation;
   

    public bool IfPause = false;
    


    void Update()
    {
        if(IfPause)
        {
            StartPause();
        }
        
        else if (!IfPause)
        {
            StartResume();
        }
    }

    public void StartPause()
    {
        PauseButton.SetActive(false);
        MenuNavigation.SetActive(true);
        IfPause = true;
        Time.timeScale = 0f;
    }

    public void StartResume()
    {
        PauseButton.SetActive(true);
        MenuNavigation.SetActive(false);
        IfPause = false;
        Time.timeScale = 1f;
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    

   

    public void StartQuit()
    {
        Application.Quit();
    }
}
