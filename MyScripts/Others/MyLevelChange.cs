using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyLevelChange : MonoBehaviour
{
    public int _currentLevel = 1;

    [SerializeField]
    private Button level1;
    [SerializeField]
    private Button level2;
    [SerializeField]
    private Button level3;
    [SerializeField]
    private Button level4;
    [SerializeField]
    private Button level5;

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        level1.interactable = true;
        level2.interactable = true;
        level3.interactable = true;
        level4.interactable = true;
        level5.interactable = true;
    }

    private void Update()
    {
        switch (_currentLevel)
        { 
            case 1:

                level1.interactable = true;
                level2.interactable = false;
                level3.interactable = false;
                level4.interactable = false;
                level5.interactable = false;
                break;

            case 2:

                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = false;
                level4.interactable = false;
                level5.interactable = false;
                break;

            case 3:

                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = false;
                level5.interactable = false;
                break;

            case 4:

                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = false;
                break;

            case 5:

                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                break;

        }

    }

}
