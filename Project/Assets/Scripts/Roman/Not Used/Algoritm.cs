using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algoritm : MonoBehaviour
{
    int[] arr = new int [5] { 9, 8, 1, 7, 3 };

    // Start is called before the first frame update
    void Start()
    {
        int smallest, temp;

        for(int i = 0; i < 5; i++ )
        {
            smallest = arr[0];

            for(int j = i +1; j < 5; j++)
            {
                if(arr[i] < smallest)
                {
                    smallest = arr[j];
                }

                temp = smallest;
                smallest = arr[i];
                    arr[i] = temp;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
