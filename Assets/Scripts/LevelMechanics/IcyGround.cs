using UnityEngine;

public class IcyGround : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("The lower the number, the more slippery it is. I didnt feel like inverting it")]
    [SerializeField] float Slipperness;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearDamping = Slipperness;
            collision.gameObject.GetComponent<playerController>().currTile = playerController.currentTile.icy;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearDamping = 1; // needs to be default value, make it auto later
            collision.gameObject.GetComponent<playerController>().currTile = playerController.currentTile.normal;
        }
    }
}
