using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Hurt,
        Dead
    }

    private State _currentState;

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _groundCheckDistance, _wallCheckDistance;
    [SerializeField]
    private LayerMask WIground , WIwall;
    private bool _groundDetected;
    private bool _wallDetected;
    private int facingDirection;
    private Vector2 _movement;

    [Header("Components")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform touchDamageCheck;

    private Animator aliveAnim,deadAnim;
    private GameObject alive,dead,drop;
    private Rigidbody2D aliveRb,deadRb;

    [Header("Combat")]
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _hurtDuration;
    [SerializeField]
    private Vector2 _hurtSpeed;//Knockback when get attacked
    private float _currentHealth;
    private float _hurtStartTime;
    private int _damageDirection;

    private float[] _attackDetails = new float[2];
    
    [Header("Touch Damage")]
    [SerializeField]
    private float _touchDamageCooldown;//To make a cooldonw between taking damage from hits
    [SerializeField]
    private float _touchDamage;//Damage to player if he touches this enemy
    [SerializeField]
    private float _touchDamageWidth;//To tune zone where touch will be detected 
    [SerializeField]
    private float _touchDamageHeight;//To tune zone where touch will be detected 
    [SerializeField]
    private LayerMask WIPlayer;

    private float _lastTouchDamageTime;
    
    private Vector2 _touchDamageBotLeft , _touchDamageTopRight;


    private void Start()
    {

        facingDirection = 1;

        _currentHealth = _maxHealth;


        alive = transform.Find("Alive").gameObject;

        drop = transform.Find("Potion").gameObject;

        dead = transform.Find("Dead").gameObject;

        aliveRb = alive.GetComponent<Rigidbody2D>();

        deadRb = dead.GetComponent<Rigidbody2D>();

        aliveAnim = alive.GetComponent<Animator>();

        deadAnim = dead.GetComponent<Animator>();

        alive.SetActive(true);
        drop.SetActive(false);
        dead.SetActive(false);
    }

    private void Update()
    {
       

        switch(_currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;

            case State.Hurt:
                UpdateHurtState();
                break;

            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //WALKING STATE ---------------------------------------------------------------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        _groundDetected = Physics2D.Raycast( groundCheck.position , Vector2.down, _groundCheckDistance, WIground);

        _wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, _wallCheckDistance, WIwall);

        CheckTouchDamage(); 

        if( !_groundDetected  || _wallDetected )
        {
            Flip();
        }
        else
        {
            Move();
        }
    }

    private void ExitWalkingState()
    {

    }

    //HURT STATE ---------------------------------------------------------------------------

    private void EnterHurtState()
    {
        _hurtStartTime = Time.time;

        //detect knockback location
        _movement.Set( _hurtSpeed.x * _damageDirection , _hurtSpeed.y );
        //apply knockback
        aliveRb.velocity = _movement;

        aliveAnim.SetBool("Knockback",true);

    }

    private void UpdateHurtState()
    {
        if(Time.time >= _hurtStartTime + _hurtDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitHurtState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    //DEAD STATE ---------------------------------------------------------------------------

    private void EnterDeadState()
    {
        alive.SetActive(false);
        dead.SetActive(true);

        dead.transform.position = alive.transform.position;

        deadRb.velocity = new Vector2(_hurtSpeed.x * _damageDirection, _hurtSpeed.y);
    }

    private void UpdateDeadState()
    {
        
    }

    private void ExitDeadState()
    {

    }

    //OTHER FUNCTIONS ---------------------------------------------------------------------------------

    private void Damage( float[] attackDetails )
    {
        _currentHealth -= attackDetails[0]; // I am going always to send the attack damage in the first index of the array
        
        //Checking the damage direction to know in which direction the enemy have to knockback after the attack
        if(attackDetails[1] > alive.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        if(_currentHealth > 0.0f)
        {
            SwitchState(State.Hurt);
        }else
        if(_currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }

    }

    private void Move()
    {
        _movement.Set(_moveSpeed * facingDirection, aliveRb.velocity.y);
        aliveRb.velocity = _movement;
    }

 private void CheckTouchDamage()
    {
        // if it passed enough time to after target got damaged,to damage it again
        if(Time.time >= _lastTouchDamageTime + _touchDamageCooldown)
        {
            _touchDamageBotLeft.Set( touchDamageCheck.position.x - (_touchDamageWidth/2) , touchDamageCheck.position.y - (_touchDamageHeight/2) );
            _touchDamageTopRight.Set(touchDamageCheck.position.x + (_touchDamageWidth / 2), touchDamageCheck.position.y + (_touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea( _touchDamageBotLeft , _touchDamageTopRight , WIPlayer );
            //If overlap circle detected Player
            if(hit != null)
            {
                _lastTouchDamageTime = Time.time;
                _attackDetails[0] = _touchDamage;
                _attackDetails[1] = alive.transform.position.x;

                hit.SendMessage("Damage", _attackDetails);
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;

        alive.transform.Rotate(0.0f , 180.0f , 0.0f);

    }

    private void SwitchState(State state)
    {
        //Wyhodim iz aktualnogo stata
        switch(_currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Hurt:
                ExitHurtState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        //Perehodim w state kotoryi nam nyzhno
        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Hurt:
                EnterHurtState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        _currentState = state;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - _groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position , new Vector2(wallCheck.position.x + _wallCheckDistance, wallCheck.position.y));

        Vector2 BotRight = new Vector2(touchDamageCheck.position.x + (_touchDamageWidth / 2), touchDamageCheck.position.y - (_touchDamageHeight / 2));
        Vector2 BotLeft = new Vector2(touchDamageCheck.position.x - (_touchDamageWidth / 2), touchDamageCheck.position.y - (_touchDamageHeight / 2));
        Vector2 TopRight = new Vector2(touchDamageCheck.position.x + (_touchDamageWidth / 2), touchDamageCheck.position.y + (_touchDamageHeight / 2));
        Vector2 TopLeft = new Vector2(touchDamageCheck.position.x - (_touchDamageWidth / 2), touchDamageCheck.position.y + (_touchDamageHeight / 2));

        Gizmos.DrawLine(BotLeft , BotRight);
        Gizmos.DrawLine(BotRight , TopRight);
        Gizmos.DrawLine(TopRight, TopLeft);
        Gizmos.DrawLine(TopLeft, BotLeft);
    }
    
}
