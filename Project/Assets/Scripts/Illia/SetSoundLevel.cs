using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetSoundLevel : MonoBehaviour
{
    public AudioMixer mixere;

    public void SetLevel(float sliderValue)
    {
        mixere.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
    }
}
