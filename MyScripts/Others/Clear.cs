using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{

    [SerializeField]
    private GameObject aliveGO;
    [SerializeField]
    private GameObject dropGO;

    private int _random_number = 0;
    private void Start()
    {
        
    }

    public void DeleteEnemy()
   {
        aliveGO.SetActive(false);

        _random_number = UnityEngine.Random.Range(1, 100);

        if(_random_number >= 70)
        {
            dropGO.SetActive(true);
        }

    }
}
