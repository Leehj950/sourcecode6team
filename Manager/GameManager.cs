using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Player player;
    
    public Player Player { get { return player; } set { player = value; } }


    // 시작하는 함수
    public void OnStartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 종료 함수
    public void OnExitBtn()
    {
        Application.Quit();
    }
}
