using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class UIButtonController : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource dash;
    public AudioSource attack;

    public void JumpPlay()
    {
        jump.Play();
       
    }
    public void DashPlay()
    {
        dash.Play();
        
    }
    public void AttcakPlay()
    {
        attack.Play();
        
    }
}
