using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private GameObject playerGO;

    private PlayerController _pc;

    private Animator _pcAnim;

    private float _currentHealth;

    [SerializeField]
    private Image _bar;

    private float _currentProcentOfHP;

    private float _maxFill = 1f;

    private void Update()
    {
        CheckHealthBar();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentProcentOfHP = 100;
        _bar.fillAmount = _maxFill;

        _pc = playerGO.GetComponent<PlayerController>();

        _pcAnim = _pc.GetComponent<Animator>();
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        _currentProcentOfHP = (_currentHealth / _maxHealth) * 100;

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public bool GetDeathStatus()
    {
        if (_currentHealth <= 0.0f)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void CheckHealthBar()
    {
        _bar.fillAmount = (_maxFill / 100) * _currentProcentOfHP;
    }
  
    private void Die()
    {   
        _pcAnim.SetBool("isDead",true);
    }

    private void Heal()
    {
        _currentHealth += _maxHealth / 4;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
            _currentProcentOfHP = 100;
        }else
        {
            _currentProcentOfHP = (_currentHealth / _maxHealth) * 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Potion")
        {
            Destroy(collision.gameObject);
            Heal();
        }
    }
}
