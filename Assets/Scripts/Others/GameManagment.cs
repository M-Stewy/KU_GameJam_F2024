using UnityEditor.SearchService;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public static GameManagment Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    AudioSource musicSource;

    public bool isPaused;
    public string PrevScene;
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }


    public void ChangeMusic(AudioClip newMu)
    {
        if (newMu != musicSource.clip)
        {
            Debug.Log("they arent the same?");
            musicSource.Stop();
            musicSource.clip = newMu;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
