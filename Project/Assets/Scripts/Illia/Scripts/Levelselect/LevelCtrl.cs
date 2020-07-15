using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCtrl : MonoBehaviour
{
    public static LevelCtrl instance = null;
    int sceneNum, levelPass;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        sceneNum = SceneManager.GetActiveScene().buildIndex;
        levelPass = PlayerPrefs.GetInt("levelPass");
    }

    public void youWin()
    {
        if (sceneNum == 5)
        {
            Invoke("loadMainMenu", 0f);
        }
        else
        {
            if (levelPass < sceneNum)
            {
                PlayerPrefs.SetInt("levelPass", sceneNum);
            }
            Invoke("loadNextLevel", 0f);
        }
    }

    void loadNextLevel()
    {
        SceneManager.LoadScene(sceneNum + 1);
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
