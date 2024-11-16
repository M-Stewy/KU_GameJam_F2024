using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StompThwomp : MonoBehaviour
{
    [SerializeField] float DropSpeed;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector2 raycastBoxSize;
    Vector3 startPos;
    Vector2 prevPos;
    Rigidbody2D rb;
    RaycastHit2D rayHit;
    Vector2 moveDir;
    bool canDrop = true;
    bool canReturn;
    enum Direction
    {
        up = 0, down = 1, left = 2, right = 3,
    }
    [SerializeField] Direction dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;

        switch (dir)
        {
            case Direction.up:
                moveDir = Vector2.up;
                break;
            case Direction.down:
                moveDir = Vector2.down;
                break;
            case Direction.left:
                moveDir = Vector2.left;
                break;
            case Direction.right:
                moveDir = Vector2.right;
                break;
        }
    }

    private void Update()
    {
        if (CheckForPlayer() && canDrop)
        {
            Debug.Log("starting Drop???");
            StartCoroutine(ThwompDrop() );
        }
        if (!CheckForPlayer() && canReturn) {
            canDrop = true;
            StartCoroutine(ReturnToStart() );
        }
    }
    bool CheckForPlayer()
    {
        rayHit = Physics2D.BoxCast(transform.position,raycastBoxSize,0,moveDir, Mathf.Infinity, playerLayer);
        return rayHit.collider != null;
    }
    private void OnDrawGizmos()
    {
        if(CheckForPlayer() )
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + -transform.up * rayHit.distance, raycastBoxSize);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + -transform.up * 99999, raycastBoxSize);
        }
    }

    IEnumerator ThwompDrop()
    {
        if(prevPos == (Vector2)transform.position)
        {
            canReturn = true;
            StopCoroutine(ThwompDrop());
        }
        else
        {
            prevPos = transform.position;
            canDrop = false;
            rb.linearVelocity = (moveDir * DropSpeed);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ThwompDrop());
        }
        
    }

    IEnumerator ReturnToStart()
    {
        Debug.Log((transform.position - startPos).magnitude);

        canReturn = false;
        if((transform.position - startPos).magnitude < 1 && (transform.position - startPos).magnitude > -1)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = startPos;
            canDrop = true;
        }
        else
        {
            rb.linearVelocity = (-moveDir * DropSpeed);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ReturnToStart());
        }
           
    }

}
