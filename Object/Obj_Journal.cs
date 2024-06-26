using System.Collections;
using System.Text;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Obj_Journal : MonoBehaviour, IPrompt, IInteract
{
    public Obj_SO Data;
    public ScriptSO script;

    [SerializeField] public Sprite paper;
    [SerializeField] private TextMeshProUGUI tmp;

    [SerializeField] private GameObject BookOpen;
    [SerializeField] private GameObject BookClose;

    // private bool isKeyDown = false;

    public void SetPromptData()
    {
        DialogueManager.Instance.prompt.Data = Data;
        //DialogueManager.Instance.prompt.PromptText.text = Data.promptText;
        DialogueManager.Instance.prompt.worldPosition = transform.position;
        //DialogueManager.Instance.prompt.curInteractIPrompt = this;
    }

    public void Interact()
    {
        DialogueManager.Instance.ReadJournal(paper, tmp);
        BookOpen?.SetActive(true);
        BookClose?.SetActive(false);
    }



    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && isKeyDown )
    //    {
    //        Interact();
    //    }
    //}


    //public void IsKeyDown()
    //{ 
    //    isKeyDown = true;
    //}
    //public void IsKeyUp()
    //{
    //    isKeyDown = false;
    //}
}
