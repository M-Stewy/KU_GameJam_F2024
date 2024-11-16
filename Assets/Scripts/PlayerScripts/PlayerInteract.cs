using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    playerController PC;

    [SerializeField] LayerMask interactableLayer;
    [SerializeField] Vector2 interactableArea;
    [SerializeField] float interactableDistance;
    Vector2 dir;
    RaycastHit2D raycastHit;

    private void Start()
    {
        PC = GetComponent<playerController>();
    }

    private void Update()
    {
        if (PC.facingRight) dir = Vector2.right;
        else dir = Vector2.left;
    }

    public void OnInteract(InputAction.CallbackContext context) 
    {
       if(context.started)
        {
            Debug.Log("Ok wgat ?");
            raycastHit = Physics2D.BoxCast(transform.position,interactableArea,0,dir,interactableDistance,interactableLayer);
            if (raycastHit.collider != null)
            {
                Debug.Log(raycastHit.collider.gameObject);
                if(raycastHit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable inter))
                {
                    Debug.Log("WHAT?!?!?");
                    inter.Interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + dir * interactableDistance, interactableArea);

    }
}
