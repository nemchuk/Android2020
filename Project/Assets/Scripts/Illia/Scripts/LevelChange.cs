using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("My components/Teleport")]
public class LevelChange : MonoBehaviour
{

    

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
