using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Vector3 direction;

    void Update()
    {
        transform.position += direction * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision == null) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        Debug.Log("DIE BULLET >:(");
        Destroy(gameObject);
    }
}
