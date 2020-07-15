using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(2);
    }
}
