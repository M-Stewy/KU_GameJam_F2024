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
            DoDash();
        }
    }

    void DoDash()
    {
        Debug.Log("test for Dash");
        rb.linearVelocity = Vector2.zero;
        if(PC.facingRight) rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
        else rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
        PC.canDash = false;
    }

}
