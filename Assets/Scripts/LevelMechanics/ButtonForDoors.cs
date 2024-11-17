using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForDoors : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource ass;

    [SerializeField] AudioClip pressButton;
    [SerializeField] AudioClip releaseButton;

    [Tooltip("List of all the doors to be moved by this button (must be the GameObject Door (#) under DoorHolder)")]
    [SerializeField] List<GameObject> DoorsToOpen = new List<GameObject>();
    // each door can be edited indvidually but only on the Door Obj, NOT the Door (#)

    [SerializeField] Sprite Unpressed;
    [SerializeField] Sprite Pressed;

    

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ass = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ass.PlayOneShot(pressButton);
        sr.sprite = Pressed;
        
        foreach (GameObject door in DoorsToOpen) // goes through all doors and moves them to their endpoints
        {
            door.GetComponentInChildren<DoorsForButton>().MoveTheDoors();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ass.PlayOneShot(releaseButton);
        sr.sprite = Unpressed;
    }

 
}
