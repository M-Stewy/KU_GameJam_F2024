using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    // this should have some animation play with it later but Dont feel like rn :(
    public void Interact()
    {
        Destroy(gameObject);
    }
}
