using UnityEditor.SearchService;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public static GameManagment Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else Instance.gameObject.SetActive(false);
    }

    public bool isPaused;
    public string PrevScene;

}
