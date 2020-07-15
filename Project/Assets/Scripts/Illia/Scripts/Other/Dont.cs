using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont : MonoBehaviour
{
    //singelton проверка того есть ли еще такие же обьекты
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
