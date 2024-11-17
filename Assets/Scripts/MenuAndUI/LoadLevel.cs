using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    private Animator Anim;

    
    public void LoadaLevel(string levelName)
    {
        StartCoroutine(TransitionEffect(levelName));
    }

    public void LoadPrevScene()
    {
        StartCoroutine(TransitionEffect(GameManagment.Instance.PrevScene));
    }

    IEnumerator TransitionEffect(string levelName)
    {
        // for transitioning into the level with animation or whatever
        Anim.SetBool("PlaySceneOpen", true);
        
        yield return new WaitForSeconds(1);
        Anim.SetBool("PlaySceneOpen", false);

        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }
}
