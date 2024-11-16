using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private CapsuleCollider2D cc;
    private Vector2 moveDir;
    [HideInInspector]
    public bool facingRight;

    [SerializeField] LayerMask GroundLayer;

    public bool isGrounded { get; private set; }
    public bool canDash;
    [HideInInspector]
    public bool jumping;
    private float jumpTimer;
    private int jumpsLeft;
    private int jumpsPressed;

    [Tooltip("The amount of frames the base jump can last")]
    [SerializeField] private float JumpTime = 60;
    [Tooltip("the force the jump applies")]
    public float jumpPower;
    [Tooltip("The amount of extra Jumps after the initial")]
    [SerializeField] int ExtraJumpAmount;
    [Tooltip("Force at which player falls")]
    [SerializeField] float FallForce;


    [SerializeField] float movespeed;

    public enum size
    {
        Small = 0,
        Medium = 1,
        Large = 2,
    }
    public size PlayerSize;

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
        if(moveDir.x > 0)
        {
            facingRight = true;
        }else if(moveDir.x < 0)
        {
            facingRight = false;
        }

        if (CheckGrounded())
        {
            ResetJumpVars();
            isGrounded = true;

        } else isGrounded = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jumping && isGrounded) Jump();
        else if(jumping && !isGrounded && jumpsPressed >= 1 && jumpsLeft > 0) AirJump();
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
        }
        if (context.canceled)
        {
            jumping = false;
            jumpTimer = JumpTime;
        }
    }

    RaycastHit2D ray;
    [SerializeField] Vector3 GroundCheckFixer;
    [SerializeField] Vector3 GroundCheckOffset;
    private bool CheckGrounded()
    {
        //GroundCheckOffset = new Vector3(GroundCheckOffset.x, cc.bounds.size.y / 2, GroundCheckOffset.z);
        //GroundCheckFixer = new Vector3(GroundCheckFixer.x, cc.size.y / 2, GroundCheckFixer.z);
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
        if (jumpTimer > 0)
        {
            jumpTimer--;
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpPower * 10));
        }
        else
        {
            jumping = false;
        }
        
    }
    private void AirJump()
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x, jumpPower * 50));
        jumpsLeft--;
        jumping = false;
    }

    private void Drop()
    {
        rb.AddForce(new Vector2(rb.linearVelocityX, -FallForce));
    }

    //used whenever something 
    public void ResetJumpVars()
    {
        jumpsLeft = ExtraJumpAmount;
        jumpsPressed = 0;
        canDash = true;
    }
}