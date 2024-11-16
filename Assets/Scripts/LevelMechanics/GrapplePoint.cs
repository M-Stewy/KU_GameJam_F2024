using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    //this isnt really nessicary at the moment, but I thought I would make this script in case we want this to be more customizable in the future (if we want specific angles or whatever)
    [SerializeField] float GrappleRadius;

    void Start()
    {
        GetComponentInChildren<CircleCollider2D>().radius = GrappleRadius;
    }

    private void OnDrawGizmos()
    {
        GetComponentInChildren<CircleCollider2D>().radius = GrappleRadius;
    }

}
