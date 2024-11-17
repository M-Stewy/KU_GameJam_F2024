using UnityEngine;
using UnityEngine.InputSystem;

public class ShrinkNGrow : MonoBehaviour
{
    Rigidbody2D rb;
    playerController PC;
    CapsuleCollider2D cc;

    Vector2 defaultCCSize;
    Vector2 defaultCCOffset;
    float defaultRBMass;
    [SerializeField] float ShrinkMultiplier;
    [SerializeField] float GrowMuliplier;

    [SerializeField] float SmallMass;
    [SerializeField] float LargeMass;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        PC = GetComponent<playerController>();

        defaultCCOffset = cc.offset;
        defaultCCSize = cc.size;
        defaultRBMass = rb.mass;
    }


    private void Update()
    {
        switch (PC.PlayerSize)
        {
            case playerController.size.Small:
                cc.size = defaultCCSize / ShrinkMultiplier;
                cc.offset = defaultCCOffset / ShrinkMultiplier;
                rb.mass = SmallMass;
                break;
            case playerController.size.Medium:
                cc.size = defaultCCSize;
                cc.offset = defaultCCOffset;
                rb.mass = defaultRBMass;
                break;
            case playerController.size.Large:
                cc.size = defaultCCSize * ShrinkMultiplier;
                cc.offset = defaultCCOffset * ShrinkMultiplier;
                rb.mass = LargeMass;
                break;
        }
    }

    public void OnShirnk(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            
            if(PC.PlayerSize == playerController.size.Large)
            {
                PC.PlayerOtherSFXs("Shrink");
                PC.PlayerSize = playerController.size.Medium;
            }
            else if(PC.PlayerSize == playerController.size.Medium) 
            {
                PC.PlayerOtherSFXs("Shrink");
                PC.PlayerSize = playerController.size.Small;
            }

        }
    }

    public void OnGrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
            if (PC.PlayerSize == playerController.size.Medium)
            {
                PC.PlayerOtherSFXs("Grow");
                PC.PlayerSize = playerController.size.Large;
            }
            else if (PC.PlayerSize == playerController.size.Small)
            {
                PC.PlayerOtherSFXs("Grow");
                PC.PlayerSize = playerController.size.Medium;
            }
        }
    }  
}
