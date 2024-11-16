using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private CapsuleCollider2D cc;
    private Vector2 moveDir;


    [SerializeField] LayerMask GroundLayer;

    private bool isGrounded;
    private bool canJump;
    private bool jumping;
    private float jumpTimer;
    private int jumpsLeft;
    private int jumpsPressed;

    [SerializeField] private float JumpTime = 60;
    [SerializeField] float jumpPower;
    [SerializeField] int ExtraJumpAmount;
    [SerializeField] float FallForce;


    [SerializeField] float movespeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        jumpsLeft = ExtraJumpAmount;
        jumpTimer = JumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckGrounded())
        {
            jumpsLeft = ExtraJumpAmount;
            jumpsPressed = 0;
            isGrounded = true;

        } else isGrounded = false;

        if (isGrounded)
        {
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jumping && isGrounded) Jump();
        else if(jumping && !isGrounded && jumpsPressed == 1) AirJump();
        if(!jumping && !isGrounded)  Drop();
        //Debug.Log(jumping);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveDir = context.ReadValue<Vector2>();
            Debug.Log(moveDir);
        }
        if (context.canceled)
        {
            moveDir = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            jumpsPressed++;
            jumping = true;
            Debug.Log("testingjumpP" + jumpsPressed);
        }
        if (context.canceled)
        {
            jumping = false;
            jumpTimer = JumpTime;
            Debug.Log("Stop Jump1");
        }
    }


    RaycastHit2D ray;
    [SerializeField] Vector3 GroundCheckFixer;
    [SerializeField] Vector3 GroundCheckOffset;
    private bool CheckGrounded()
    {
        GroundCheckOffset = new Vector3(GroundCheckOffset.x, cc.bounds.size.y / 2, GroundCheckOffset.z);
        GroundCheckFixer = new Vector3(GroundCheckFixer.x, cc.size.y / 2 + .5f, GroundCheckFixer.z);
        ray = Physics2D.BoxCast(cc.bounds.center - GroundCheckOffset, cc.bounds.size - GroundCheckFixer, 0, Vector2.down, 0.1f, GroundLayer);
        return ray.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (CheckGrounded())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cc.bounds.center - GroundCheckOffset + -transform.up * ray.distance, cc.bounds.size - GroundCheckFixer);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cc.bounds.center - GroundCheckOffset + -transform.up * 0.1f, cc.bounds.size - GroundCheckFixer);
        }
    }

    private void MovePlayer()
    {
        rb.AddForce(new Vector2(moveDir.x * movespeed,rb.linearVelocity.y));
    }

    private void Jump()
    {
        Debug.Log("Jumping1");
        if (jumpTimer > 0)
        {
            jumpTimer--;
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpPower * 10));
        }
        else
        {
            jumping = false;
            Debug.Log("Stop Jump2");
        }
        
    }
    private void AirJump()
    {
        Debug.Log("Jumping2");
        rb.AddForce(new Vector2(rb.linearVelocity.x, jumpPower * 50));
        jumpsLeft--;
        jumping = false;
    }

    private void Drop()
    {
        rb.AddForce(new Vector2(rb.linearVelocityX, -FallForce));
    }
}
