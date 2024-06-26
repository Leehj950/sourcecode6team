using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public Obj_SO Data; // TODO:: SO클래스 바꾸기
    public TextMeshProUGUI PromptText; 
    private StringBuilder sb;       // 여기에 상호작용오브젝트의 텍스트가 담김
    public Vector3 worldPosition;   // 여기에 상호작용오브젝트의 월드포지션이 담김

    //public IPrompt curInteractIPrompt; // 현재 상호작용한 오브젝트의 IPrompt를 담음.

    public float Ycorrection = 0.5f;

    private bool isActive;

    private void Start()
    {
        PromptText = GetComponentInChildren<TextMeshProUGUI>();
        sb = new StringBuilder();
        isActive = false;
    }

    

    public void PrintPrompt()
    {
        if (isActive) return;
        isActive = true;

        // UI 캔버스 - Render Mode - World Space.

        // 출력 위치 설정
        // 출력하는 객체의 월드포지션을 얻음.
        Vector3 worldPos = worldPosition;
        // 생성위치 오브젝트의 위로 조금 올리기

        PromptText.transform.position = worldPos + new Vector3(0, Ycorrection, 0);

        // 스트링 넣기
        sb.Append(Data.promptText);
        PromptText.text = sb.ToString();

        StartCoroutine(coroutine());

        // 코루틴으로 글씨의 위치가 위로 올라감, 투명해지면서 사라짐.
        IEnumerator coroutine()
        {

            yield return new WaitForSeconds(1f);

            // 글씨의 위치가 위로 올라감, 투명해지면서 사라짐.
            float elapsedTime = 0f;
            Vector3 originPos = PromptText.transform.position;
            Color originColor = PromptText.color;
            //float Yoftmp;
            float height = 0.5f;


            // 위치와 색을 점차 변경
            while (elapsedTime < Data.TextFadeOutSpeed)
            {
                elapsedTime += Time.deltaTime;

                float ySet = Mathf.Lerp(0, height, elapsedTime / Data.TextFadeOutSpeed);
                PromptText.transform.position = originPos + new Vector3(0, ySet, 0);

                Color color = PromptText.color;
                color.a = Mathf.Lerp(1f, 0f, elapsedTime / Data.TextFadeOutSpeed);
                PromptText.color = color;
                yield return null;
            }
            // while문 실행이 멈추면 아래코드 실행


            PromptText.text = sb.Remove(0, sb.Length).ToString();

            // 원래 위치로 되돌림
            PromptText.transform.position = originPos;
            PromptText.color = originColor;
            isActive = false;
            yield return null;
        }
    }
}
