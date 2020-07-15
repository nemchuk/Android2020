using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevelCtrl : MonoBehaviour
{
    public Button level2, level3, level4, level5;
    int levelPass;

    void Start()
    {
        levelPass = PlayerPrefs.GetInt("LevelPass");
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;

        switch (levelPass)
        {
            case 1:
                level2.interactable = true;
                break;
            case 2:
                level2.interactable = true;
                level3.interactable = true;
                break;
            case 3:
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                break;
            case 4:
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                break;
        }
    }

   public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }

}
