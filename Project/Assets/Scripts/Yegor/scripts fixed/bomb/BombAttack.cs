using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    //explode on collision
    void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Target")
                Destroy(coll.gameObject);
                Destroy(gameObject);
        }
}