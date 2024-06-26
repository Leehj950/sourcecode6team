using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteract
{
    public GameObject flashlight;
    public ScriptSO ScriptSO;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(ScriptSO);
            GetFlashlight();
        }
    }


    void GetFlashlight()
    {
        flashlight.SetActive(true);

        gameObject.SetActive(false);
    }

    public void Interact()
    {
        GetFlashlight();
    }
}
