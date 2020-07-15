using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Joystick joystick;//place for a joystick in Inspector

    private Animator _anim;//place for animator in Inspector
    private Rigidbody2D _rb;//declare rb

    [SerializeField]
    private Transform groundCheck;//place for groundchecker
    [SerializeField]
    private Transform wallCheck;//place for wallchecker
    [SerializeField]
    private GameObject player;

    private PlayerStats _ps;

    private GameManager _GM;

    [Header("Horizontal Movement")]
    [SerializeField]
    private float moveSpeed = 10.0f;//var to tune player move speed ( assigned  value is random )

    private float _direction; // to store what direction is player moving
    private bool _walking;//Detect moving for playing walk animation
    private bool _facingRight = true; //to detect a player looking direction
    private bool _canFlip;
    private string _horizontal = "Horizontal";
    private string _walkingAnim = "walking";
    private string _jumpingAnim = "jumping";


    [Header("Vertical Movement")]
    [SerializeField]
    private float jumpForce = 16.0f;

    private bool _canJump;
    private bool _jumping;//for playing jump animation
    private bool _isGrounded;//for detecting collision with ground
    private bool _isDead;

    [SerializeField]
    private int jumpsNumber = 1;//to make possible multiple jumps(1 by default)

    private int _jumpsLeft;// to calculate how many jumps can player actually do

    [SerializeField]
    private float airMoveForce;//
    [SerializeField]
    private float airDrag;

    [Header("Ground Check")]
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask WIGround;

    [Header("Wall Check")]
    [SerializeField]
    private float distance;// distance for wall to be detected

    private int _facingDirection = 1;// will store 1 for right and -1 for left ( 1 bydefault because character is turned right by default)
    private bool _isTouchingWall;//for decting collision with walls
    private bool _isWallSliding;//for detecting if character is wall sliding

    [SerializeField]
    private float wallSlideSpeed;// to tune the wall slide speed
    [SerializeField]
    private LayerMask WIWall;// wall layermask
    [SerializeField]
    private Vector2 wallJumpDirection;
    [SerializeField]
    private float wallJumpForce;// Store the force we gonna  to jump  with(from the wall)

    [Header("Dash")]
    [SerializeField]
    private float dashTime;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashCooldown;

    [Header("KnockBack")]
    [SerializeField]
    private float knockbackDuration;
    [SerializeField]
    private Vector2 knockbackSpeed;

    private float _knockbackStartTime;

    private bool _knockback;

    [Header("Sounds")]
    public AudioSource dashSound;
    public AudioSource jumpSound;
    public AudioSource walkSound;

   

    private bool _isDashing;
    private float _dashTimeLeft;
    private float _lastDash = -100;


    void Start()
    {
        _isDead = false;
        _rb = GetComponent<Rigidbody2D>();// rb reference 
        _anim = GetComponent<Animator>();//Animator reference
        _jumpsLeft = jumpsNumber;//setting amount of jumps depending on number in inspector
        wallJumpDirection.Normalize();//vector itself = 1 and will be multiplied by our scepcified float wallHopForce to tune it from inspector 

        _ps = GetComponent<PlayerStats>();


    }

    private void FixedUpdate()
    {
        ApplyMovement();//realisation of movement
        Flip();//fliping direction of the charackter depending on what direction player is trying to move
        HandleAnimations();//realisation of charackter animations
        CheckSurround();//realisation of Surrounding check
        CheckWallSlide();//returns true if we re wall sliding and false if not
    }
    void Update()
    {
        CheckIfIsDead();
        CheckKnockback();
        HandleJoystick();//makes moving with joystick more comfortable
        CheckIfCanJump();//returns true if we can perform a jump and false if not
        CheckDash();//returns true if we can dash and false if not
    }

    private void Clear()
    {
        player.SetActive(false);
        
    }


    private void Flip()
    {
        if (!_isWallSliding && !_isDashing && !_knockback && !_isDead) // in order not to flip sprite when sliding wall 
        {
            if ((_facingRight && _direction < 0) || (!_facingRight && _direction > 0))
            {
                _facingDirection *= -1;
                _facingRight = !_facingRight;
                transform.Rotate(0, 180, 0);
            }
        }

    }

    private void CheckIfIsDead()
    {
        _isDead = _ps.GetDeathStatus();
    }

    public bool GetDashStatus()
    {
        if(_isDashing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Knockback(int direction)
    {
        _knockback = true;

        _knockbackStartTime = Time.time;

        _rb.velocity = new Vector2( knockbackSpeed.x * direction , knockbackSpeed.y );
    }

    private void CheckKnockback()
    {
        if( ( Time.time >= (_knockbackStartTime + knockbackDuration) ) && _knockback && !_isDead)
        {
            _knockback = false;

            _rb.velocity = new Vector2( 0.0f , _rb.velocity.y  );
        }
    }

    //I have maden this function public in order it to be visable in  OnClickEvent in UI Button "JumpButton"'s Inspector

    public void Dash()
    {
        if(Time.time >= ( _lastDash + dashCooldown ) )
        {
            _isDashing = true;
            _dashTimeLeft = dashTime;
            _lastDash = Time.time;
       
        }
    }

    private void CheckDash()
    {
        if(_isDashing && !_isDead)
        {
           if(_dashTimeLeft > 0)
            {
                //dash
                _rb.velocity = new Vector2(dashSpeed * _facingDirection, 0);
                _dashTimeLeft -= Time.deltaTime;
                DashSound();
                
            }

           if(_dashTimeLeft <= 0 || _isTouchingWall)
            {
                _isDashing = false;
            }
        }    
    }

    private void CheckWallSlide()
    {
        if(_isTouchingWall && !_isGrounded && _rb.velocity.y < 0 && !_isDead)
        {
            _isWallSliding = true;
        }else
        {
            _isWallSliding = false;
        }
    }

    //for Combat script
    public int GetFacingDirection()
    {
        return _facingDirection;
    }

    private void ApplyMovement()
    {
        if (!_isDashing && !_isDead)
        {
            if (_isGrounded && !_knockback && !_isDead) // in order not to be able to move in X velocity when wall sliding
            {
                //movement
                _rb.velocity = new Vector2(_direction, _rb.velocity.y);//change the position of a player on X axes and Y stays as it was
                
            }
            else
        if (!_isGrounded && !_isWallSliding && _direction != 0 && !_knockback && !_isDead) // in case we move in the air
            {
                _rb.AddForce(new Vector2(airMoveForce * _direction, 0));

                if (Math.Abs(_rb.velocity.x) > moveSpeed)
                {
                    _rb.velocity = new Vector2(_direction, _rb.velocity.y);

                }
            }
            else
        if (!_isGrounded && !_isWallSliding && _direction == 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x * airDrag, _rb.velocity.y);
                

            }
        }

        //detecting horizontal movement for playing walk animation
        if (_rb.velocity.x != 0)
        {
            _walking = true;
        }
        else
        {
            _walking = false;
        }

        //same for jump animation

        if(_rb.velocity.y != 0)
        {
            _jumping = true;
        }else
        {
            _jumping = false;
        }

        if(_isWallSliding)
        {
            if(_rb.velocity.y < -wallSlideSpeed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    //I have maden this function public in order it to be visable in  OnClickEvent in UI Button "JumpButton"'s Inspector
    public void Jump()
    {
        
        NormalJump();
        WallJump();
        
    }

    private void NormalJump()
    {
        if (_canJump && !_isWallSliding)//default jump
        {
            //jump
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpsLeft--;
            JumpSound();
        }
    }

    private void WallJump()
    {
        if ((_isWallSliding || _isTouchingWall) && (_direction != 0) && _canJump) // wall jump
        {
            if ((_direction < 0) && (_facingDirection == 1))
            {
                _isWallSliding = false;
                _jumpsLeft--;
                //wall jump
                _rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * _direction, wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);
                JumpSound();

                
            }
            else
            if ((_direction > 0) && (_facingDirection == -1))
            {
                _isWallSliding = false;
                _jumpsLeft--;

                _rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * _direction, wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);

            }
            else
            {

            }
        }
    }

    private void CheckIfCanJump()
    {
        if ( (_isGrounded && _rb.velocity.y <= 0) || _isWallSliding )
        {
            _jumpsLeft = jumpsNumber;

        }
        if (_jumpsLeft <= 0 )
        {
            _canJump = false;
        }else
        {
            _canJump = true;
        }
    }

    private void DisableFlip()
    {
        _canFlip = false;
    }

    private void EnableFlip()
    {
        _canFlip = true;
    }

    private void HandleJoystick()
    {
        if (joystick.Horizontal >= .2f)
        {
            _direction = moveSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            _direction = -moveSpeed;
        }else if (Input.GetAxisRaw(_horizontal) >= .2f)
        {
            _direction = moveSpeed;
        }else if (Input.GetAxisRaw(_horizontal) <= -.2f)
        {
            _direction = -moveSpeed;
        }
        else
        {
            _direction = 0f;
        }

        if(!_isWallSliding && !_isDashing)
        {
            _canFlip = true;
        }
        else
        {
            _canFlip = false;
        }
    }

    private void HandleAnimations()
    {   
        _anim.SetBool(_walkingAnim, _walking);

        _anim.SetBool(_jumpingAnim, _jumping);
    }

    //To check for collisions with ground,walls, ... ,etc.
    private void CheckSurround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, WIGround);

        _isTouchingWall = Physics2D.Raycast(wallCheck.position , transform.right , distance , WIWall );//postion of wallchekck on rb,right of a character,
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);// to visualise collision with ground

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + distance, wallCheck.position.y, wallCheck.position.z));
    }


    public void JumpSound()
    {
        jumpSound.Play();
    }

    public void DashSound()
    {
        dashSound.Play();
    }

    public void WalkSound()
    {
        walkSound.Play();
            
    }

   
}
