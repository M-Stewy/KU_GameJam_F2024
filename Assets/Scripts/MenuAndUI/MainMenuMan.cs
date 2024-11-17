using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMan : MonoBehaviour
{

    LoadLevel level;
    private void Start()
    {
        level =  GetComponent<LoadLevel>();
    }

    public void StartGame()// opens level select with some sort of transisiton before it loads
    {
        level.LoadaLevel("LevelSelect");
    }


    public void Settings() //Open the settings menu with some sort of transisiton before it loads
    {
        level.LoadaLevel("Settings");
    }

    public void Controls() // Opens the Controls menu with some sort of transisiton before it loads
    {
        level.LoadaLevel("Controls");
    }
    

    public void Credits()
    {
        level.LoadaLevel("Credits");
    }

    public void QuitGame() // quits game with some sort of transisiton before
    {
        StartCoroutine(QuitTransition());
    }
    IEnumerator QuitTransition()
    {
        level.LoadaLevel("MainMenu");
        yield return new WaitForSeconds(1); 
        Application.Quit();
    }
}
