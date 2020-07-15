using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private bool canHit;
    [SerializeField]
    private float inputTimer;

    private AttackDetails _attackDetails;

    private bool _gotInput, _attacking;
    private float _lastInputTime = Mathf.NegativeInfinity;// to store time when player last tryied to attemp hit

    [Header("Components")]
    [SerializeField]
    private LayerMask WIDamage;

    private Animator _anim;
    private PlayerController _pc;
    private PlayerStats _ps;


    [Header("Melee Attack")]
    [SerializeField]
    private Transform attack1HitBox;
    [SerializeField]
    private float attack1Radius , attack1Damage;
    [SerializeField]
    private float stunDamage = 1f;

    private string _attackAnim = "canAttack";
    private string _attack1Anim = "attack1";
    private string _attackingAnim = "attacking";
    private string _damage = "Damage";


    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool(_attackAnim,canHit);

        _pc = GetComponent<PlayerController>();
        _ps = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        
        CheckAttacks();
    }

    //this function is public to be visible in UI Button OnClick event
    public void CheckCombatInput()
    {
        if(canHit)
        {
            _gotInput = true;

            _lastInputTime = Time.time;
        }  
    }

    private void CheckAttacks()
    {
        if(_gotInput)
        {
            if(!_attacking)
            {
                _gotInput = false;
                _attacking = true;
                _anim.SetBool(_attack1Anim, true);
                _anim.SetBool(_attackingAnim, _attacking);
            }
        }

        if(Time.time >= _lastInputTime + inputTimer)
        {
            _gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll( attack1HitBox.position , attack1Radius , WIDamage );

        _attackDetails.damage = attack1Damage;
        _attackDetails.position = transform.position;
        _attackDetails.stunDamage = stunDamage;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage(_damage, _attackDetails);
        }
    }

    private void FinishAttack1()
    {
        _attacking = false;
        _anim.SetBool(_attackingAnim, false);
        _anim.SetBool(_attack1Anim, false);
    }
    
    private void Damage(AttackDetails p_attackDetails)
    {
        //Player doesn t receive damage when dash through the enemies
        if( !_pc.GetDashStatus() )
        {
            int direction;

            _ps.DecreaseHealth(p_attackDetails.damage);

            if (p_attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            _pc.Knockback(direction);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
}
