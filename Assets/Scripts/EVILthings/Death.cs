using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    playerController PC;
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        PC = FindAnyObjectByType<playerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision) //for when we want it to be solid
    {
        if (collision.collider.CompareTag("Player"))
        {
            PC = collision.collider.gameObject.GetComponent<playerController>();
            StartCoroutine(deathCutscene());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider) // for when we want to to not be solid
    {
        if (collider.CompareTag("Player"))
        {
            PC = collider.gameObject.GetComponent<playerController>();
            StartCoroutine(deathCutscene());
        }
            
    }

    IEnumerator deathCutscene()
    {
        PC.KillSelf();
        // play the cutscene or whatever we want to do before reloading
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene.name);
    }
}
