using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length, height, startPosX, startPosY;
    
    public float parallaxEffect;
    [SerializeField] bool DoLooping;
    public GameObject play;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    
    void FixedUpdate()
    {
        float tempX = (play.transform.position.x *(1 - parallaxEffect));
        

        float distanceX = (play.transform.position.x * parallaxEffect);
        
        
        transform.position = new Vector3(startPosX + -distanceX,startPosY, transform.position.z);
        if (!DoLooping) return;

        if(tempX > startPosX + length) { startPosX += length; }
        else if(tempX < startPosX - length) {  startPosX -= length; }

        
    }

}
