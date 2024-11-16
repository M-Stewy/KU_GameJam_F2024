using System.Collections;
using UnityEngine;

public class ShootTurret : MonoBehaviour
{
    [SerializeField] float timeBetweenShots;
    [SerializeField] GameObject BulletsToShoot;
    [SerializeField] float bulletSpeed;

    GameObject curBullet;
    Transform shootPoint;
    enum direction
    {
        up = 0, down = 1, left = 2, right = 3
    }
    [SerializeField] direction dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShootBullet());
        shootPoint = GetComponentInChildren<Transform>();

        
    }

    IEnumerator ShootBullet()
    {

        yield return new WaitForSeconds(1);
        curBullet = Instantiate(BulletsToShoot,shootPoint.position,Quaternion.identity);
        switch (dir)
        {
            case direction.up:
                curBullet.GetComponent<BulletMover>().direction = -Vector3.down * bulletSpeed;
                curBullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case direction.down:
                curBullet.GetComponent<BulletMover>().direction = Vector3.down * bulletSpeed;
                curBullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case direction.left:
                curBullet.GetComponent<BulletMover>().direction = Vector3.left * bulletSpeed;
                curBullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case direction.right:
                curBullet.GetComponent<BulletMover>().direction = -Vector3.right * bulletSpeed;
                curBullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
        
        StartCoroutine(ShootBullet());
    }
    
}
