using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AimRotation :MonoBehaviour
{
    // 캐릭터 관련
    [SerializeField] private SpriteRenderer chatRenderer;
    
    // 후레쉬 관련
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform pivot;


    private CharController controller;
    private void Awake()
    {
        controller = GetComponent<CharController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 vector)
    {
        RotateAim(vector);
    }

    private void RotateAim(Vector2 vector)
    {
        // 라디안으로 변경
        float rotz = Mathf.Atan2(vector.y,vector.x) * Mathf.Rad2Deg;

        // 90도를 넘지 않기 위한.
        chatRenderer.flipX = Mathf.Abs(rotz) > 90f;

        // 좌우 변경 조건문
        if (chatRenderer.flipX == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }

        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        chatRenderer.flipX = spriteRenderer.flipX;

        pivot.rotation = Quaternion.Euler(0,0,rotz);
    }
}
