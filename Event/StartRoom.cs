using UnityEngine;

public class StartRoom : MonoBehaviour
{
    public ScriptSO Script;
    bool isTriggered = false;
    public AudioClip clip;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isTriggered && collision.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(Script);

        }
    }

    
}
