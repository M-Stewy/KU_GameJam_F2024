using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    // this should have some particle effect play with it later but Dont feel like rn :(
    [SerializeField] AudioClip destrutionNoise;
    AudioSource ass;

    SpriteRenderer sr;
    BoxCollider2D bc;

    [SerializeField] Vector2 size;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        bc.size = size;
        sr.size = size;
        ass = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        AudioSource.PlayClipAtPoint(destrutionNoise,transform.position, Random.Range(0.75f,1.1f));
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ass.pitch = Random.Range(.8f, 1.2f);
        ass.volume = Random.Range(.25f,0.75f);
        ass.Play();
        
    }
}
