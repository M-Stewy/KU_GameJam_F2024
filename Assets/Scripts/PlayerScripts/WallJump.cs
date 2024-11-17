using UnityEngine;

public class WallJump : MonoBehaviour
{
    playerController PC;
    [SerializeField] CapsuleCollider2D cc;
    Rigidbody2D rb;

    [SerializeField] LayerMask wallLayer;
    bool touchingWall;
    bool facingRight;


    [SerializeField] float jumpPowerHor;
    [SerializeField] float jumpPowerVer;
    private void Start()
    {
        PC = GetComponent<playerController>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        facingRight = PC.facingRight;
        touchingWall = WallCheck();

        if (touchingWall)
        {
            
            PC.ResetJumpVars(false);
        }
        
            
        
    }

    private void FixedUpdate()
    {
        if(touchingWall && PC.jumping)
        {
            PC.animator.SetBool("JumpedOffWall", false);
            PC.animator.SetBool("isOnWall", true);
            DoWallJump();
        }
        if (!touchingWall)
        {
            PC.animator.SetBool("isOnWall", false);
        }
        


        
    }

    void DoWallJump()
    {
        if(facingRight)
        {
            rb.linearVelocity = new Vector2(-jumpPowerHor, jumpPowerVer);
            //rb.AddForce(new Vector2(-jumpPowerHor, jumpPowerVer),ForceMode2D.Impulse);
        }
        else
        {
            rb.linearVelocity = new Vector2(jumpPowerHor, jumpPowerVer);
            //rb.AddForce(new Vector2(jumpPowerHor, jumpPowerVer),ForceMode2D.Impulse);
        }
        
        PC.jumping = false;
    }

    RaycastHit2D hit;
    [SerializeField] Vector3 WallCheckOffset;
    [SerializeField] Vector3 WallCheckFixer;
    bool WallCheck() 
    { 
        if(facingRight) WallCheckOffset.x = -Mathf.Abs(WallCheckOffset.x); //sets offset to right(neg val) when facing right, left(pos val) otherwise
        else WallCheckOffset.x = Mathf.Abs(WallCheckOffset.x);

        hit = Physics2D.BoxCast(cc.bounds.center - WallCheckOffset, cc.bounds.size - WallCheckFixer, 0, Vector2.down, 0.1f, wallLayer);
        return hit.collider != null;
    }

    //Debuging stuff to visualize it in editor :)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (WallCheck())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cc.bounds.center - WallCheckOffset + -transform.up * hit.distance, cc.bounds.size - WallCheckFixer);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cc.bounds.center - WallCheckOffset + -transform.up * 0.1f, cc.bounds.size - WallCheckFixer);
        }
    }
}
