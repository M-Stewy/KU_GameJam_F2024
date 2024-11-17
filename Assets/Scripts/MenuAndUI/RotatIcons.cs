using System.Collections;
using UnityEngine;

public class RotatIcons : MonoBehaviour
{
    public float rotateNum, rotateSpeed, rotateAng, vertSway;


    bool goingLeft;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, rotateAng * rotateNum);
        //transform.position = new Vector3 (transform.position.x,transform.position.y - vertSway,transform.position.z);
        transform.Translate(0, -vertSway * rotateNum, 0, Space.World);
        StartCoroutine(wobble());
    }

    private IEnumerator wobble()
    {
        while (true)
        {

            for (int i = 0; i < rotateNum; i++)
            {
                transform.Rotate(0, 0, -rotateAng * 2);
                transform.Translate(0, vertSway * 2, 0, Space.World);
                yield return new WaitForSeconds(rotateSpeed);
            }
            for (int i = 0; i < rotateNum; i++)
            {
                transform.Rotate(0, 0, rotateAng * 2);
                transform.Translate(0, -vertSway * 2, 0, Space.World);
                yield return new WaitForSeconds(rotateSpeed);
            }
        }


    }


}
