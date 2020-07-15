using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All enemies are going do derive from this class
public class Entity : MonoBehaviour
{

    //List of components all the enemies are going to have in common

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;//Stores all the variables like ground check distance/Layermasks/.../etc

    public Rigidbody2D aliveRb { get; private set; }
    public Rigidbody2D deadRb { get; private set; }
    public Animator aliveAnim { get; private set; }
    public Animator deadAnim { get; private set; }
    public Animator dropAnim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int facingDirection { get; private set; }



    private Vector2 _velocitySpace;

    [SerializeField]
    private Transform wallCkeck;

    [SerializeField]
    private GameObject _alive;

    [SerializeField]
    private Transform ledgeCheck;

    [SerializeField]
    private Transform playerCheck;

    [SerializeField]
    private Transform groundCheck;

    private float _currentHealth;

    private string _yVelocity = "yVelocity";

    private float _currentStunResistance;

    private float _lastDamageTime;

    public int lastDamageDirection { get; private set; }

    protected bool isStunned;
    protected bool isDead;


    public virtual void Start()
    {

        facingDirection = 1;

        //References to "Alive" GO/Animator/RigidBody2D

        aliveGO = _alive;

        aliveRb = aliveGO.GetComponent<Rigidbody2D>();
        aliveAnim = aliveGO.GetComponent<Animator>();   

        //Every entity is going to have its own state machine which is an instance of FiniteStateMachine class
        stateMachine = new FiniteStateMachine();

        //To have acces to functions from Pink to Alive Animator(for setting the animation events for attacks)
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();

        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        aliveAnim.SetFloat( _yVelocity , aliveRb.velocity.y );

        if(Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
           
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float p_velocity)
    {
        _velocitySpace.Set( facingDirection * p_velocity , aliveRb.velocity.y );

        aliveRb.velocity = _velocitySpace;
    }

    public virtual void SetVelocity(float _velocity , Vector2 _angle , int _direction)
    {
        _angle.Normalize();

        _velocitySpace.Set( _angle.x * _velocity * _direction , _angle.y * _velocity );

        aliveRb.velocity = _velocitySpace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast( wallCkeck.position  , aliveGO.transform.right , entityData.wallCheckDistance , entityData.whatIsWall );
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle( groundCheck.position , entityData.groundCheckRadius , entityData.whatIsGround );
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast( ledgeCheck.position , Vector2.down , entityData.ledgeCheckDistance , entityData.whatIsGround );
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast( playerCheck.position , aliveGO.transform.right  , entityData.minAgroDistance , entityData.whatIsPlayer );
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right , entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerForCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position , aliveGO.transform.right  , entityData.closeRangeActionDistance , entityData.whatIsPlayer);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails p_attackDetails)
    {

        _lastDamageTime = Time.time;

        _currentHealth -= p_attackDetails.damage;

        _currentStunResistance -= p_attackDetails.stunDamage;

        Hop(entityData.hopSpeedX , entityData.hopSpeedY);

        if(p_attackDetails.position.x > aliveGO.transform.parent.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(_currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if(_currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void Hop(float _xVelocity , float  _yVelocity)
    {
        _velocitySpace.Set( _xVelocity * -facingDirection, _yVelocity);

        aliveRb.velocity = _velocitySpace;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;

        aliveGO.transform.Rotate( 0f , 180f , 0f );
    }

    public virtual void  OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCkeck.position , wallCkeck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance  ) );


        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance ), 0.2f);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection *  entityData.minAgroDistance ), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance) , 0.2f);
    }

}
