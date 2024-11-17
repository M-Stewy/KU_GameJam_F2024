using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class Sign : MonoBehaviour, IInteractable
{
    public GameObject obj;
    public bool signOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    { 
            if (signOpen)
            {
            obj.SetActive(false);
            signOpen = false;
            }
            else
            {
            obj.SetActive(true);
            signOpen = true;
            }   
    }

    void OnTriggerExit2D(Collider2D cd)
    {
        if(cd.CompareTag("Player"))
        {
            obj.SetActive(false);
            signOpen = false;
        }
    }

}
