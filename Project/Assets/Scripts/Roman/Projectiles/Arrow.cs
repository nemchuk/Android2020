using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private AttackDetails _attackDetails;

    private float _speed;
    private float _xStartPos;
    private float _travelDistance;

    private bool _isGravityOn;
    private bool _isHitGround;
    private string _damage = "Damage";

    private Rigidbody2D _rb;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _isGravityOn = false;

        _rb.gravityScale = 0.0f;
        _rb.velocity = transform.right * _speed;

        _xStartPos = transform.position.x;
    }

    private void Update()
    {
        if(!_isHitGround)
        {

            _attackDetails.position = transform.position;

            if(_isGravityOn)
            {
                float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
      if(!_isHitGround)
        {

            Collider2D damageHit = Physics2D.OverlapCircle( damagePosition.position , damageRadius , whatIsPlayer );
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if(damageHit)
            {
                damageHit.transform.SendMessage(_damage , _attackDetails);
                Destroy(gameObject);
            }

            if(groundHit)
            {
                _isHitGround = true;
                _rb.gravityScale = 0f;
                _rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_isGravityOn)
            {
                _isGravityOn = true;

                _rb.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile( float p_speed , float p_travelDistance , float p_damage )
    {
        _speed = p_speed;
        _travelDistance = p_travelDistance;
        _attackDetails.damage = p_damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position , damageRadius);
    }
}
