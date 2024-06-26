using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DialogueManager : MonoBehaviour
{
    // 다이얼로그를 호출하는 창구 클래스
    // 대화창을 띄울 경우의 호출
    // DialogueManager.Instance.호출메서드
    public static DialogueManager Instance;

    public Prompt prompt;
    public Dialogue dialogue;
    public Journal journal;
   
    

    private void Awake()
    {
        Instance = this;
    }


    public void StartDialogue(ScriptSO scriptSO)
    {
        dialogue.StartDialogue(scriptSO);
        //DialogueManager.Instance.StartDialogue(scriptSO);
    }

    // 오브젝트에서 저장된 종이이미지와 글을 받는다.
    public void ReadJournal(Sprite paperImage, TextMeshProUGUI tmp)
    {
        journal.paperImage = paperImage;
        journal.Text.text = tmp.text;
        journal.ActiveJournal();
    }


}
public interface IPrompt
{
    // 프롬프트에 출력스트링과 위치정보를 넣어주는 메서드
    public void SetPromptData();
    // 작성예시
    //public void SetPromptData()
    //{
    //    DialogueManager.Instance.prompt.PromptText.text = SO.prompText;
    //    DialogueManager.Instance.prompt.worldPosition = transform.position;
    //}
}