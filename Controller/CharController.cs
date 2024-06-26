using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;


    // 이동에 대한 
    public void CallMoveEvent(Vector2 moveVec)
    {
        OnMoveEvent?.Invoke(moveVec);
    }

    //카메라에 대한
    public void CallLookEvent(Vector2 LookVec)
    {
        OnLookEvent?.Invoke(LookVec);
    }
}
