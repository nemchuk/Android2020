using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable object means a container we may may store data in whuch is independet from classes instances
//I am going to use it to store all the variables related to my MoveState amd than create differnet copies of it for different enemies

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]

public class D_MoveState : ScriptableObject
{
    public float moveSpeed = 3f;
}
