using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    // this should have some particle effect play with it later but Dont feel like rn :(
    [SerializeField] AudioClip destrutionNoise;
    AudioSource ass;
    private void Start()
    {
        ass = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        AudioSource.PlayClipAtPoint(destrutionNoise,transform.position, Random.Range(0.75f,1.1f));
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ass.pitch = Random.Range(-.25f, .25f);
        ass.volume = Random.Range(.25f,0.75f);
        ass.Play();
        
    }
}
