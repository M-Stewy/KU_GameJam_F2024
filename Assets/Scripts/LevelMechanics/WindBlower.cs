using UnityEngine;

public class WindBlower : MonoBehaviour
{
    AreaEffector2D ae;

    [SerializeField] enum dir //will also use this to determine which direction the sprite animates
    {
        up =0, down = 1, left = 2, right = 3,
    }
    [Tooltip("Direction the player moves in when within the wind box")]
    [SerializeField] dir windDirection;

    [Tooltip("speed at which the player is pushed by the wind")]
    [SerializeField] float windSpeed;

    [Tooltip("how much resistence is applied to the player")] // higher = player can move less within the wind
    [SerializeField] float windDrag;                         // but too low and there is too much variation when floating


    private void Start()
    {
        ae = GetComponent<AreaEffector2D>();
        switch (windDirection)
        {
            case dir.up:
                ae.forceAngle = 90;
                break;
           case dir.down:
                ae.forceAngle = 270;
                break; 
            case dir.left:
                ae.forceAngle = 180;
                break; 
            case dir.right:
                ae.forceAngle = 0;
                break;
        }
        ae.forceMagnitude = windSpeed;
        ae.drag = windDrag;
    }


}
