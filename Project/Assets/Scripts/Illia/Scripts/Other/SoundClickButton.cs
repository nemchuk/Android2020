using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class SoundClickButton : MonoBehaviour
{
    public AudioClip sound;
    private AudioMixerGroup SoundMixer;


    private Button button
    {
        get
        {
            return GetComponent<Button>();
        }
    }

    private AudioSource source
    {
        get
        {
            return GetComponent<AudioSource>();

        }
    }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
