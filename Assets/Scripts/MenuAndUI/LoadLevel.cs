using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
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
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }
}
