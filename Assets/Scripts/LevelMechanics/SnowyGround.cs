using UnityEngine;

public class SnowyGround : MonoBehaviour
{
    [Range(1f, 5f)]
    [Tooltip("The higher the number the more resistence the player feels when trying to move")]
    [SerializeField] float SlowDownFactor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearDamping = SlowDownFactor;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().linearDamping = 1; // needs to be default value, make it auto later
    }
}
