using NUnit.Framework;
using System.Collections.Generic;
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
    public List<string> CompletedStages { get; private set; } = new List<string>();

    public List<string> CompletedStages { get; private set; }   = new List<string>();

    public bool isPaused;
    public string PrevScene;
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void AddToCompleted(string completed)
    {
        foreach (string stage in CompletedStages)
        {
            if (stage == completed) return;
        }

        CompletedStages.Add(completed);
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
