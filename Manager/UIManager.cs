using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    // 컨디션을 관리 하기 위한  
    public Condition hpCondtion;
    public Condition stamina;
    public Condition lightBar;

    //메뉴판 올리기 위한 
    public GameObject menuPanel;

    // 메뉴판 사용하기위한 
    bool isBtn = false;

    private void Update()
    {
        if (GameManager.Instance.Player.Controller.IsMenuWindow == true)
        {
            OnMenuPanelBtn();
        }
    }

    // 메뉴판 함수
    public void OnMenuPanelBtn()
    {
        // 버튼 활성화랑 비 활성화
        isBtn = !isBtn;
        //메뉴얼 비활성하는 것입니다.
        menuPanel.SetActive(isBtn);
        // 시간 스케일을 변경합니다.
        Time.timeScale = isBtn ? 0.0f : 1.0f; 
    }
    // 현재 씬 다시 
    public void OnReStrat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void OnMainMenuBtn()
    {
        SceneManager.LoadScene("IntroScene");
    }

}
