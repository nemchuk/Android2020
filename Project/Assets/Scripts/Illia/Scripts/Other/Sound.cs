using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public AudioSource clickOnMenu;
    public AudioSource clickOnUi;
    public AudioMixer sound;

    public void PlayclickOnMenu()
    {
        clickOnMenu.Play();
    }

    public void PlayclickOnUi()
    {
        clickOnUi.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
