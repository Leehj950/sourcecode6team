using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public ScriptSO scriptSO;   // 이 대화에 쓰일 대사문
    public ScriptSO[] DebugGarageSO;

    public GameObject DialogueCanvas;

    StringBuilder sbBody = new StringBuilder();
    StringBuilder sbSpeaker = new StringBuilder();

    public TextMeshProUGUI bodyText;
    public TextMeshProUGUI speakerText;
    public Image portrait;

    private IEnumerator curPrintLine;

    private float typingPerSeconds = 40f;
    public float TypingPerSeconds 
    {
        get { return typingPerSeconds; } 
        set { typingPerSeconds = Mathf.Max(value, 5f); } 
    }

    private bool skipRequested = false;

    private bool isActive = false;

    // scriptSO에서 정보를 로드시킴.    
    public void InitScript(ScriptSO _script)
    {
        scriptSO = _script;
    }

    // 다이얼로그 활성화
    public void OpenDialogue()
    {
        DialogueCanvas.SetActive(true);
    }
    public void CloseDialogue()
    {
        DialogueCanvas.SetActive(false);
    }

    // scriptSO의 BodyScripts 길이만큼 텍스트를 출력
    public IEnumerator PrintScript()
    {
        //speakerText.text = sbSpeaker.Remove(0, sbSpeaker.Length).ToString();
        speakerText.text = sbSpeaker.Clear().ToString();
        //bodyText.text = sbBody.Remove(0, sbBody.Length).ToString();
        bodyText.text = sbBody.Clear().ToString();
        portrait.sprite = null;

        for ( int i = 0; i < scriptSO.bodyScripts.Length; i++)
        {           
            // 요소 넣기
            //speakerText.text = sbSpeaker.Append(scriptSO.speakers[i]).ToString();
            UtilSB.SetText(speakerText, sbSpeaker, scriptSO.speakers[i]);
            //portrait.sprite = scriptSO.images[i];
            SetImageAndMaintainAspectRatio(portrait, scriptSO.images[i]);
            if(portrait.sprite == null)
            {
                portrait.transform.localScale = Vector3.zero;
            }
            else { portrait.transform.localScale = Vector3.one; }
            //bodyText.text = sbBody.Append(scriptSO.bodyScripts[i]).ToString();
            curPrintLine = PrintBodyTextPerSeconds(scriptSO.bodyScripts[i]);
            yield return StartCoroutine(curPrintLine);
            if(!skipRequested)
            {
                yield return new WaitUntil(() => GameManager.Instance.Player.Controller.IsTalk ); 
            }
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Enter or Space");

            //speakerText.text = sbSpeaker.Clear().ToString();
            UtilSB.ClearText(speakerText, sbSpeaker);
            //bodyText.text = sbBody.Clear().ToString();
            UtilSB.ClearText(bodyText, sbBody);
            portrait.sprite = null;
        }
        CloseDialogue();

        
        isActive = false;

        yield return null;
    }

    public void StartDialogue(ScriptSO scriptSO)
    {
        if (isActive) return;
        isActive = true;

        OpenDialogue();
        InitScript(scriptSO);
        StartCoroutine(PrintScript());
    }

    public void TypeSpeedUp()
    {
        TypingPerSeconds += 5f;
    }
    public void TypeSpeedDown()
    {
        TypingPerSeconds -= 5f;
    }

    // DebugTest
    public void StartDialogueButton()
    {
        if (isActive) return;
        isActive = true;

        OpenDialogue();
        InitScript(DebugGarageSO[0]);
        StartCoroutine(PrintScript());
    }
    public void StartDialogueButton1()
    {
        if (isActive) return;
        isActive = true;

        OpenDialogue();
        InitScript(DebugGarageSO[1]);
        StartCoroutine(PrintScript());
    }



    // 본문 타이핑효과 출력
    IEnumerator PrintBodyTextPerSeconds(string SOstr)
    {
        //bodyText.text = "";
        //sbBody.Clear();
        UtilSB.ClearText(bodyText, sbBody);
        
        foreach (char c in SOstr.ToCharArray())
        {
            if (GameManager.Instance.Player.Controller.IsTalk)
            {
                // Append the remaining string immediately and exit the coroutine
                Debug.Log("skip");
                skipRequested = true;
                break;
            }
            //sbBody.Append(c);
            //bodyText.text = sbBody.ToString();
            UtilSB.AppendText(bodyText, sbBody, c);
            yield return new WaitForSeconds(1f / typingPerSeconds);
        }
        if(skipRequested)
        {
            //bodyText.text = SOstr;
            UtilSB.SetText(bodyText, sbBody, SOstr);
            skipRequested = false;            
        }
        yield return null;
    }



    // AspectRatioFitter 컴포넌트를 이용한 이미지 비율조정.
    public void SetImageAndMaintainAspectRatio(Image image, Sprite sprite)
    {
        if (image == null || sprite == null)
            return;

        // Set the new sprite
        image.sprite = sprite;

        // Add AspectRatioFitter component if it doesn't exist
        AspectRatioFitter aspectRatioFitter = image.GetComponent<AspectRatioFitter>();
        if (aspectRatioFitter == null)
        {
            aspectRatioFitter = image.gameObject.AddComponent<AspectRatioFitter>();
        }

        // Set the aspect ratio to the sprite's aspect ratio
        aspectRatioFitter.aspectRatio = (float)sprite.rect.width / sprite.rect.height;

        // Set the aspect mode
        aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
    }
}
