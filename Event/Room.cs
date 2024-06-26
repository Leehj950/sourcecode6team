using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Room : MonoBehaviour
{
    public ScriptSO Script;
    bool isTriggered = false;
    public AudioClip clip;
    public GameObject openingMV;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isTriggered && collision.CompareTag("Player"))
        {
            isTriggered = true;
            if (openingMV)
            {
                StartCoroutine(movie());
            }

            //DialogueManager.Instance.StartDialogue(Script);

        }
    }

    IEnumerator movie()
    {
        openingMV.SetActive(true);
        float Length = (float)openingMV.GetComponent<VideoPlayer>().clip.length + 0.5f;
        yield return new WaitForSeconds(Length);
        openingMV.SetActive(false);

        DialogueManager.Instance.StartDialogue(Script);
    }
}