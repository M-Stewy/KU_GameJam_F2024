using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    bool isDead = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField]
    private CapsuleCollider2D cc;

    private Vector2 moveDir;
    [HideInInspector]
    public bool facingRight;

    [SerializeField] LayerMask GroundLayer;
    public Animator animator; 
    public bool isGrounded { get; private set; }
    public bool canDash;
    public bool StopGrav;
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

    [SerializeField] float JumpMoveSpeedMultipler;


    [SerializeField] float movespeed;


    [Header("Sound effects")]
    AudioSource[] asses;
    [SerializeField] AudioClip deathNoise1;
    [SerializeField] AudioClip deathNoise2;
    [SerializeField] AudioClip LandedSFX;
    [SerializeField] AudioClip NormalGroundWalk;
    [SerializeField] AudioClip IcyWalk;
    [SerializeField] AudioClip SnowyWalk;

    public AudioClip DashSFX;
    public AudioClip WallJumpSFX;

    public AudioClip ShrinkSFX;
    public AudioClip GrowSFX;

    public AudioClip Grapple1;
    public AudioClip Grapple2;
    public AudioClip Grapple3;

    public enum currentTile
    {
        normal = 0,
        icy = 1,
        snowy = 2
    }
    public currentTile currTile;


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
        sr = GetComponentInChildren<SpriteRenderer>();

        jumpsLeft = ExtraJumpAmount;
        jumpTimer = JumpTime;
        asses = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        /*
        if(rb.linearVelocity.x < 0.01 || rb.linearVelocity.x > 0.01)
        {
            animator.SetBool("isMoving", true);
            if (rb.linearVelocity.x > 0.01)
            {
                sr.flipX = true;
            }
            else if (rb.linearVelocity.x < 0.01)
            {
                sr.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        */

        if(Mathf.Round(moveDir.x) > 0)
        {
            facingRight = true;
            sr.flipX = true;
            animator.SetBool("isMoving", true);
        }else if(Mathf.Round(moveDir.x) < 0)
        {
            sr.flipX = false; 
            animator.SetBool("isMoving", true);
            facingRight = false;
        }
        else if (Mathf.Round(moveDir.x) == 0)
        {
            animator.SetBool("isMoving", false);
        }

        if(rb.linearVelocity.y > 0.01)
        {
            animator.SetFloat("jumpVelocity", rb.linearVelocity.y);
        }
        else if(rb.linearVelocity.y < 0.01)
        {
            animator.SetFloat("jumpVelocity", rb.linearVelocity.y);
        }
        if (CheckGrounded())
        {
            ResetJumpVars();
            isGrounded = true;
            canDash = true;
            animator.SetBool("isGrounded", isGrounded);
        }
        else {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }

        if (isGrounded && moveDir.x != 0) PlayWalkSFX(false);
        if(moveDir.x == 0 || !isGrounded) PlayWalkSFX(true);

        if(PlayerSize == size.Medium) { sr.transform.localScale = Vector3.one; }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        MovePlayer();

        if (jumping && isGrounded) Jump();
        else if(jumping && !isGrounded && jumpsPressed >= 1 && jumpsLeft > 0) AirJump();
        if(!jumping && !isGrounded)  Drop();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveDir = context.ReadValue<Vector2>();
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
            if (isGrounded) asses[1].Play();
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
        if(isGrounded)
            rb.AddForce(new Vector2(moveDir.x * movespeed,rb.linearVelocity.y));
        else
            rb.AddForce(new Vector2(moveDir.x * movespeed * JumpMoveSpeedMultipler, rb.linearVelocity.y));
    }

    private void Jump()
    {
        if (jumpTimer > 0)
        {
            jumpTimer--;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
        }
        else
        {
            jumping = false;
        }
        
    }
    private void AirJump()
    {
        asses[1].Play();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower * 1.25f);
        jumpsLeft--;
        jumping = false;
    }

    private void Drop()
    {
        if (StopGrav) return;
        rb.AddForce(new Vector2(rb.linearVelocityX, -FallForce));
    }

    //used whenever something 
    public void ResetJumpVars(bool stillDash = false)
    {
        jumpsLeft = ExtraJumpAmount;
        jumpsPressed = 0;
        if(stillDash) canDash = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stop"> stops the function </param>
    public void PlayWalkSFX(bool stop = false)
    {
        if (stop) { 
            StartCoroutine(lowerVolume(true, 0));
            return;
        }
        if (asses[0].isPlaying) return;
        asses[0].volume = 0.55f;
        switch (currTile)
        {
            case currentTile.normal:
                asses[0].PlayOneShot(NormalGroundWalk);
                break;    
            case currentTile.icy:
                asses[0].PlayOneShot(IcyWalk);
                break;
            case currentTile.snowy:
                asses[0].PlayOneShot(SnowyWalk);
                break;
        }
    }

    public void PlayerOtherSFXs(string sfxToPlay)
    {
        if (asses[2].isPlaying) return;
        switch (sfxToPlay)
        {
            case "Dash":
                asses[2].volume = .46f;
                asses[2].PlayOneShot(DashSFX);
                break;
            case "WallJump":
                asses[2].volume = 1.1f;
                asses[2].PlayOneShot(WallJumpSFX);
                break;
            case "Grapple":
                asses[2].volume = 1f;
                asses[2].PlayOneShot(Grapple2);
                break;
            case "Shrink":
                asses[2].volume = 1.75f;
                asses[2].PlayOneShot(ShrinkSFX);
                break;
            case "Grow":
                asses[2].volume = 1.75f;
                asses[2].PlayOneShot(GrowSFX);
                break;
        }
    }

    IEnumerator lowerVolume(bool lower, int index)
    {
        if (lower)
        {
           if(asses[index].volume > 0) 
           { 
                asses[index].volume -= 0.01f;
                yield return new WaitForSeconds(0.1f);
                lowerVolume(true, index);
           } else asses[index].Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        asses[1].PlayOneShot(LandedSFX);
    }


    public void KillSelf() // makes it so the player can not do anything at all anymore because thats what happens when you die :O
    {
        if (Random.Range(0, 2) == 0) AudioSource.PlayClipAtPoint(deathNoise2, transform.position, 1.2f);
        else AudioSource.PlayClipAtPoint(deathNoise1, transform.position, 1.1f);

        isDead = true;
        if(TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            input.enabled = false;  
        }
        if(TryGetComponent<Dash>(out Dash d))
        {
            d.enabled = false;
        }
        if(TryGetComponent<WallJump>(out WallJump wal))
        {
            wal.enabled = false;
        }
        if (TryGetComponent<Grapple>(out Grapple g))
        {
            g.enabled = false;
        }
        if (TryGetComponent<ShrinkNGrow>(out ShrinkNGrow sng))
        {
            sng.enabled = false;
        }
        if(TryGetComponent<PlayerInteract>(out PlayerInteract pi))
        {
            pi.enabled = false;
        }

    }
}