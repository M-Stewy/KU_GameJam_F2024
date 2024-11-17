using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    playerController PC;
    Rigidbody2D rb;
    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    bool dashing;
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started) dashing = true;
        else if(context.canceled) dashing = false;
    }

    private void Start()
    {
        PC = GetComponent<playerController>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(dashing && PC.canDash)
        {
            StartCoroutine(DoDash() );
        }
    }

    IEnumerator DoDash()
    {
        PC.PlayerOtherSFXs("Dash");
        PC.canDash = false;
        PC.StopGrav = true;
        Debug.Log("test for Dash");
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        if(PC.facingRight) rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
        else rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        PC.StopGrav = false;
        rb.gravityScale = 1;
    }

}