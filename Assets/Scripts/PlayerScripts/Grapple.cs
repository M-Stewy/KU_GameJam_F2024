using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    playerController PC;
    DistanceJoint2D dj;
    LineRenderer lr;
    GameObject graplePoint;
   
    bool canGrap;
    bool isGrappling;

    [SerializeField] float GrappleReelSpeed;


    private void Start()
    {
        dj = GetComponent<DistanceJoint2D>();
        lr = GetComponent<LineRenderer>();
        PC = GetComponent<playerController>();
    }

    private void Update()
    {
        if (isGrappling)
        {
            lr.SetPosition(0,transform.position);
            lr.SetPosition(1,graplePoint.transform.position);
        }

        if(dj.distance <= 1) StopGrappling();
        if(!isGrappling) StopGrappling();
        if(PC.jumping)  StopGrappling(); 
        if(!PC.canDash) StopGrappling();


    }

    private void FixedUpdate()
    {
        if (isGrappling && dj.distance > 0)
        {
            dj.distance -= GrappleReelSpeed / 10;
        }
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("Grappleing");
            if(canGrap && !isGrappling)
            {
                ConnectPlayerToPoint(graplePoint);
            }
            else if(isGrappling)
            {
                isGrappling = false;
            }
            
        }
    }

    public void ConnectPlayerToPoint(GameObject point)
    {
        isGrappling = true;
        PC.ResetJumpVars();

        dj.distance = Vector2.Distance(transform.position, point.transform.position);
        dj.enabled = true;
        dj.connectedAnchor = point.transform.position;
            
        lr.enabled = true;
        lr.SetPosition(1, point.transform.position);
    }

    public void StopGrappling()
    {
        dj.enabled = false;
        lr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrappleZone"))
        {
            Debug.Log("Entered Grapl Z");
            canGrap = true;
            graplePoint = collision.transform.parent.gameObject;
            Debug.Log(graplePoint.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GrappleZone"))
        {
            canGrap = false;
        }
    }
}
