using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : CharController
{
    /// <summary>
    /// Value
    /// </summary>
    private PlayerInput input;
    private new Camera camera;
    private bool isLight;
    private bool isTalk;
    private bool isUseItme;
    private bool isRuning;
    private bool isMeunWindow;
    /// <summary>
    /// Property
    /// </summary>
    // 후레쉬 상태 체크
    public bool IsLight { get { return isLight; } set { isLight = value; } }
    // 대화 체크
    public bool IsTalk { get { return isTalk; } set { isTalk = value; } }
    // 아이템 사용 체크
    public bool IsUseItme { get { return isUseItme; } set { isUseItme = value; } }

    // 달리기 상태 체크
    public bool IsRuning { get { return isRuning; } set { isRuning = value; } }

    public bool IsMenuWindow { get { return isMeunWindow; } set { isMeunWindow = value; } }

    /// <summary>
    /// Method
    /// </summary>
    private void Awake()
    {
        camera = Camera.main;
    }


    private void OnEnable()
    {
        // 할당
        input = new PlayerInput();

        // 이동 할당
        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerStop;

        // 뛰기 할당
        input.Player.Run.performed += PlayerStartRun;
        input.Player.Run.canceled += PlayerEndRun;

        // 조준점 할당
        input.Player.Look.performed += PlayerLook;

        // 대화 할당
        input.Player.Talk.performed += PlayerStartTalk;
        input.Player.Talk.canceled += PlayerEndTalk;

        // 공격 할당
        input.Player.Fire.performed += PlayerStartFire;
        input.Player.Fire.canceled += PlayerEndFire;

        // 상호작용 할당
        input.Player.UseItem.performed += PlayerStartUse;
        input.Player.UseItem.canceled += PlayerEndUse;

        input.UI.Menu.performed += MeunStart;
        input.UI.Menu.canceled += MeunEnd;

        input.UI.Enable();
        input.Player.Enable();
    }

    private void OnDisable()
    {
        // 이동해제
        input.Player.Move.performed -= PlayerMove;
        input.Player.Move.canceled -= PlayerStop;

        // 뛰기 할당
        input.Player.Run.performed -= PlayerStartRun;
        input.Player.Run.canceled -= PlayerEndRun;

        // 조준 해제
        input.Player.Look.performed -= PlayerLook;

        // 대화 해제
        input.Player.Talk.performed -= PlayerStartTalk;
        input.Player.Talk.canceled -= PlayerEndTalk;
        // 조준 해제
        input.Player.Fire.performed -= PlayerStartFire;
        input.Player.Fire.canceled -= PlayerEndFire;

        // 상호작용 해제
        input.Player.UseItem.performed -= PlayerStartUse;
        input.Player.UseItem.canceled -= PlayerEndUse;

        input.UI.Menu.performed -= MeunStart;
        input.UI.Menu.canceled -= MeunEnd;
        input.UI.Disable();
        input.Player.Disable();
    }

    private void MeunEnd(InputAction.CallbackContext context)
    {
        IsMenuWindow = false;
    }

    private void MeunStart(InputAction.CallbackContext context)
    {
        IsMenuWindow = context.ReadValueAsButton();
        Debug.Log(IsMenuWindow);
    }

    private void PlayerMove(InputAction.CallbackContext context)
    {
        Vector2 vec2 = context.ReadValue<Vector2>();

        if (Time.timeScale == 1)
        {
            CallMoveEvent(vec2);
        }
    }
    private void PlayerStartRun(InputAction.CallbackContext context)
    {
        isRuning = context.ReadValueAsButton();
    }

    private void PlayerEndRun(InputAction.CallbackContext context)
    {
        isRuning = false;
    }

    private void PlayerStop(InputAction.CallbackContext context)
    {
        Vector2 vec2 = Vector2.zero;

        CallMoveEvent(vec2);
    }

    private void PlayerLook(InputAction.CallbackContext context)
    {
        //카메라에 마우스의 진행중인 백터를 가져옵니다.
        Vector2 vec2 = context.ReadValue<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(vec2);
        vec2 = (worldPos - (Vector2)transform.position).normalized;

        // 그값을 옵저버 패턴으로 만들어진것을 이용해 값을 전달합니다.
        if (Time.timeScale == 1)
        {
            CallLookEvent(vec2);
        }
    }

    private void PlayerStartTalk(InputAction.CallbackContext context)
    {
        IsTalk = context.ReadValueAsButton();
        Debug.Log(isTalk);
    }
    private void PlayerEndTalk(InputAction.CallbackContext context)
    {
        IsTalk = false;
    }

    private void PlayerStartFire(InputAction.CallbackContext context)
    {
        IsLight = context.ReadValueAsButton();
    }
    private void PlayerEndFire(InputAction.CallbackContext context)
    {
        IsLight = false;
    }

    private void PlayerStartUse(InputAction.CallbackContext context)
    {
        GameManager.Instance.Player.PressUse();
    }
    private void PlayerEndUse(InputAction.CallbackContext context)
    {

    }

}
