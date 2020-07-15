using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Joystick joystick;//place for a joystick in Inspector
    private Animator anim;//place for animator in Inspector
    private Rigidbody2D rb;//declare rb
    [SerializeField]
    private Transform groundCheck;//place for groundchecker
    [SerializeField]
    private Transform wallCheck;//place for wallchecker

    [Header("Horizontal Movement")]
    [SerializeField]
    private float moveSpeed = 10.0f;//var to tune player move speed ( assigned  value is random )
    private float direction; // to store what direction is player moving
    private bool walking;//Detect moving for playing walk animation
    private bool facingRight = true; //to detect a player looking direction

    [Header("Vertical Movement")]
    [SerializeField]
    private float jumpForce = 16.0f;
    private bool canJump;
    private bool jumping;//for playing jump animation
    private bool isGrounded;//for detecting collision with ground
    [SerializeField]
    private int jumpsNumber = 1;//to make possible multiple jumps(1 by default)
    private int jumpsLeft;// to calculate how many jumps can player actually do
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
    private int facingDirection = 1;// will store 1 for right and -1 for left ( 1 bydefault because character is turned right by default)
    private bool isTouchingWall;//for decting collision with walls
    private bool isWallSliding;//for detecting if character is wall sliding
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

    private bool isDashing;
    private float dashTimeLeft;
    private float lastDash = -100;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// rb reference 
        anim = GetComponent<Animator>();//Animator reference
        jumpsLeft = jumpsNumber;//setting amount of jumps depending on number in inspector
        wallJumpDirection.Normalize();//vector itself = 1 and will be multiplied by our scepcified float wallHopForce to tune it from inspector 
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
        HandleJoystick();//makes moving with joystick more comfortable
        CheckIfCanJump();//returns true if we can perform a jump and false if not
        CheckDash();//returns true if we can dash and false if not
    }

    private void Flip()
    {
        if (!isWallSliding && !isDashing) // in order not to flip sprite when sliding wall 
        {
            if ((facingRight && direction < 0) || (!facingRight && direction > 0))
            {
                facingDirection *= -1;
                facingRight = !facingRight;
                transform.Rotate(0, 180, 0);
            }
        }

    }

    //I have maden this function public in order it to be visable in  OnClickEvent in UI Button "JumpButton"'s Inspector

    public void Dash()
    {
        if(Time.time >= ( lastDash + dashCooldown ) )
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDash = Time.time;
        }
    }

    private void CheckDash()
    {
        if(isDashing)
        {
           if(dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;
            }

           if(dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
            }
        }    
    }

    private void CheckWallSlide()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }else
        {
            isWallSliding = false;
        }
    }

    private void ApplyMovement()
    {
        if(!isDashing)
        {
            if (isGrounded) // in order not to be able to move in X velocity when wall sliding
            {
                rb.velocity = new Vector2(direction, rb.velocity.y);//change the position of a player on X axes and Y stays as it was
            }
            else
        if (!isGrounded && !isWallSliding && direction != 0) // in case we move in the air
            {
                rb.AddForce(new Vector2(airMoveForce * direction, 0));

                if (Math.Abs(rb.velocity.x) > moveSpeed)
                {
                    rb.velocity = new Vector2(direction, rb.velocity.y);
                }
            }
            else
        if (!isGrounded && !isWallSliding && direction == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDrag, rb.velocity.y);
            }
        }

        //detecting horizontal movement for playing walk animation
        if (rb.velocity.x != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        //same for jump animation

        if(rb.velocity.y != 0)
        {
            jumping = true;
        }else
        {
            jumping = false;
        }

        if(isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
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
        if (canJump && !isWallSliding)//default jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft--;
            
        }
    }

    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && (direction != 0) && canJump) // wall jump
        {
            if ((direction < 0) && (facingDirection == 1))
            {
                isWallSliding = false;
                jumpsLeft--;

                rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * direction, wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);

                
            }
            else
            if ((direction > 0) && (facingDirection == -1))
            {
                isWallSliding = false;
                jumpsLeft--;

                rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * direction, wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);

            }
            else
            {

            }
        }
    }

    private void CheckIfCanJump()
    {
        if ( (isGrounded && rb.velocity.y <= 0) || isWallSliding )
        {
            jumpsLeft = jumpsNumber;

        }
        if (jumpsLeft <= 0 )
        {
            canJump = false;
        }else
        {
            canJump = true;
        }
    }

    private void HandleJoystick()
    {
        if (joystick.Horizontal >= .2f)
        {
            direction = moveSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            direction = -moveSpeed;
        }else if (Input.GetAxisRaw("Horizontal") >= .2f)
        {
            direction = moveSpeed;
        }else if (Input.GetAxisRaw("Horizontal") <= -.2f)
        {
            direction = -moveSpeed;
        }
        else
        {
            direction = 0f;
        }
    }

    private void HandleAnimations()
    {   
        anim.SetBool("walking", walking);

        anim.SetBool("jumping", jumping);
    }

    //To check for collisions with ground,walls, ... ,etc.
    private void CheckSurround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, WIGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position , transform.right , distance , WIWall );//postion of wallchekck on rb,right of a character,
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);// to visualise collision with ground

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + distance, wallCheck.position.y, wallCheck.position.z));
    }
}
