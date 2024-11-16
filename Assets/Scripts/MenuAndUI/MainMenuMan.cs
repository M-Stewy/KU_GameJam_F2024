using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMan : MonoBehaviour
{
    public void StartGame()// opens level select with some sort of transisiton before it loads
    {
        StartCoroutine(startTransition());
    }
     IEnumerator startTransition()
     {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LevelSelect");
     }


    public void Settings() //Open the settings menu with some sort of transisiton before it loads
    {
        StartCoroutine(settingsTransition());
    }
    IEnumerator settingsTransition()
    {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Settings");
    }

    public void Controls() // Opens the Controls menu with some sort of transisiton before it loads
    {
        StartCoroutine (controlsTransition());
    }
    IEnumerator controlsTransition()
    {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Controls");
    }

    public void Credits()
    {
        StartCoroutine(transisitionCredits());
    }

    IEnumerator transisitionCredits()
    {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame() // quits game with some sort of transisiton before
    {
        StartCoroutine(QuitTransition());
    }
    IEnumerator QuitTransition()
    {
        yield return new WaitForSeconds(1); 
        Application.Quit();
    }
}
