using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MyNextLevel : MonoBehaviour
{
    [SerializeField]
    private int scene;

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scene);
        }
        
    }
}
