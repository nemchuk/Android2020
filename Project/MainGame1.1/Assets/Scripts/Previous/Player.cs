using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField]
    public float speed = 10f; // speed of movement
     
    private Vector2 direction;

    private bool facinRight = true; // for detecting face direction

    [Header("Vertical Movement")]
    [SerializeField]
    public float jumpForce = 15f;
    [SerializeField]
    public float jumpDelay = 0.25f;

    private float jumpTimer;

    [Header("Components")]
    [SerializeField]
    public LayerMask groundLayer; 
    [SerializeField]
    public Joystick joystick;
    private Rigidbody2D rb;

    [Header("Physics")]
    [SerializeField]
    public float maxSpeed = 7f;// to define regulate velocity 
    [SerializeField]
    public float linearDrag = 4f;
    [SerializeField]
    public float gravity = 1f;
    [SerializeField]
    public float fall = 5f;

    [Header("Collision")]
    [SerializeField]
    public bool onGround = false;
    [SerializeField]
    public float length = 0.6f;
    [SerializeField]
    public Vector3 colliderOffset;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    
    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical")); // defenition of direction reference

        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, length, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, length, groundLayer);

        //jump
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
    }

    private void FixedUpdate()
    {
        movePlayer(direction.x);// = -1 -> 1 

        if(jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        modifyPhysics();

        if(direction.x < 0 && facinRight)
        {
            Flip();
        }else
        if(direction.x > 0 && !facinRight)
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed , rb.velocity.y); // if speed > maxSpeed than speed = maxSpeed
        }
    }

    void movePlayer(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * speed);// Vector.right = ( 1 , 0 )  *  ( -1 - > 1 | right if > 0 and left if < 0)

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0); //reset the y velocity
        rb.AddForce( Vector2.up * jumpForce , ForceMode2D.Impulse );// 1st parameter = where to apply the force * how strong, 2 = instant
        jumpTimer = 0;
    }

    void modifyPhysics()
    {

        bool changinDirection = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if(onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changinDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fall;
            }
            else
            if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fall / 2);
            }
             
        }
    }

    void Flip()
    {
        facinRight = !facinRight;

        transform.rotation = Quaternion.Euler(0 , facinRight ? 0 : 180 , 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * length);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * length);
    }
}
