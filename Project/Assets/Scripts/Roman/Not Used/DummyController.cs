using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private float maxHealth;
    private float currentHealth;


    [Header("KnockBack")]
    [SerializeField]
    private bool applyKnockback;
    [SerializeField]
    private float knockbackX, knockbackY, duration;
    private bool knockback;
    private float knockBackStart;

    [Header("References")]
    private PlayerController pc;
    private GameObject aliveGO, deadGO;
    private Rigidbody2D rbAlive, rbDead;
    private Animator aliveAnim,deadAnim;
    private int playerFacingDirection;

    [Header("Death")]
    private bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>(); //goes through all hierarchy and finds our Player and than Player script

        aliveGO = transform.Find("Alive").gameObject;//goes through all the children objects of an object script is attahed to

        deadGO = transform.Find("Dead").gameObject;//goes through all the children objects of an object script is attahed to

        aliveAnim = aliveGO.GetComponent<Animator>();

        deadAnim = deadGO.GetComponent<Animator>();

        rbAlive = aliveGO.GetComponent<Rigidbody2D>();

        rbDead = deadGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        deadGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float amount)
    {
        currentHealth -= amount;
        playerFacingDirection = pc.GetFacingDirection();
        aliveAnim.SetTrigger("damage");

        if(applyKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockBackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackX * playerFacingDirection , knockbackY);
    }

    private void CheckKnockback()
    {
        if( (Time.time >= knockBackStart + duration) && knockback )
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f , rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        deadGO.SetActive(true);

        

        deadGO.transform.position = aliveGO.transform.position;

        rbDead.velocity = new Vector2(knockbackX * playerFacingDirection, knockbackY);
    }

    
}
