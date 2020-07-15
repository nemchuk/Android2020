using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{
    public GameObject settings;
    private SceneManager manager;
    public AudioSource clickSound;
    public AudioSource otherSound;
 


    public void LoadGame()
    {

    }

    public void Settings()
    {
        settings.SetActive(!settings.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void setMusic(float value)
    {
        Global.music = value;
    }

    public void setSound(float value)
    {
        Global.sounds = value;
    }

    public void LoadScene()
    {

        StartCoroutine(instObject());
        
        SceneManager.LoadScene(1);
    }

    IEnumerator instObject()
    {
        yield return new WaitForSeconds(1f);
    }
    
    public void AudioOutput()
    {
        clickSound.Play();
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void ToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }
}
