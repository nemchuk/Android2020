using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
	public int health = 100;

	public GameObject deathEffect;

	public void TakeDamage(int damage)
	{

		health -= damage;

		if (health <= 0)
		{

			Die();
		}
	}

	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		anim.SetBool("ghoustsDeath", true);
        Destroy(gameObject);
      
	}

}
