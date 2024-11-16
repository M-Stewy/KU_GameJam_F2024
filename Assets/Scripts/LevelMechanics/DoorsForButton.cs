using System.Collections;
using System.Linq;
using UnityEngine;

public class DoorsForButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] BoxCollider2D bc;


    [SerializeField] Vector2 doorSize;
    [Tooltip("how smooth the door looks as it moves")]
    [SerializeField] float doorSmoothness;
    [Tooltip("speed at which the door reaches its target, higher val = quicker movement")]
    [SerializeField] float doorSpeed;
    float doorMoveIncrement;    //increaments the door by this much every time it moves, is calculated from the dist and Smoothness val

    [SerializeField] Transform StartPos; // Should be exactly where the doors starting Transform is
    [SerializeField] Transform EndPos;  // where the door should end up at after its been moved


    Vector3 moveDir;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        sr.size = doorSize;
        bc.size = doorSize;
        moveDir = EndPos.position - StartPos.position;
        doorMoveIncrement = moveDir.magnitude / doorSmoothness;
    }

    private void OnDrawGizmos()
    {
        sr.size = doorSize;
        bc.size = doorSize;
    }

    /// <summary>
    /// Will move the doors from its start position to its end position using a coroutine
    /// </summary>
    public void MoveTheDoors()
    {
        StartCoroutine(MoveDoor());
    }


    public IEnumerator MoveDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position, EndPos.position, doorMoveIncrement);
        yield return new WaitForSeconds(1/doorSpeed);
        if(transform.position != EndPos.position)
        {
            MoveTheDoors();
        }
    }

}
