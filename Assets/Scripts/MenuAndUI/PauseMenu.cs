using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject thePauseMenu;

     public void OnPause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            PauseStuff();
        }
    }

    public void ResumeButtomPress()
    {
        PauseStuff();
    }

    void PauseStuff()
    {
        if (GameManagment.Instance == null) {
            Debug.LogWarning("THE PAUSE MENU CAN NOT WORK RIGHT NOW IT NEEDS TO START FROM THE MAIN MENU, k thanks");
            return;
        }

        if(GameManagment.Instance.isPaused)
        {
            Time.timeScale = 1;
            thePauseMenu.SetActive(false);
            GameManagment.Instance.isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            thePauseMenu.SetActive(true);
            GameManagment.Instance.isPaused = true;
        }
    }


    public void Settings() //Open the settings menu with some sort of transisiton before it loads
    {
        Time.timeScale = 1;
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
        Time.timeScale = 1;
        StartCoroutine(controlsTransition());
    }
    IEnumerator controlsTransition()
    {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Controls");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(transisitionToMenu());
    }

    IEnumerator transisitionToMenu()
    {
        yield return new WaitForSeconds(1);
        GameManagment.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MainMenu");
    }



}
