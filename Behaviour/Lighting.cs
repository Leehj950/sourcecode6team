using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lighting : MonoBehaviour
{
    // 나중에 오브젝트랑 맞나서 되는 지만 확인
    [SerializeField] Light2D light2d;
    [SerializeField] private LayerMask colliderLayer;

    //기본 후레쉬 값
    private float TagetAngleInner;
    // 공격할 후레쉬 값
    private float AttackAngelOuter = 20;

    private PlayerController controller;

    private List<Enemy> previousEnemies = new List<Enemy>();
    List<Enemy> currentEnemies = new List<Enemy>();

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        TagetAngleInner = light2d.pointLightInnerAngle;

        UIManager.Instance.lightBar.CurValue = light2d.pointLightInnerRadius;
    }

    private void Update()
    {
        if (controller.IsLight == false)
        {
            TagetLight();
        }
        else
        {
            AttackLight();
        }
    }

    private void TagetLight()
    {
        // 만약 세개 켜져있을 경우 조절하겠끔 조절한다.
        if (light2d.pointLightInnerRadius > 0.01f)
        {

            light2d.pointLightInnerRadius = Mathf.Lerp(light2d.pointLightInnerRadius, 0, Time.deltaTime);
            UIManager.Instance.lightBar.Subtract(Mathf.Lerp(light2d.pointLightInnerRadius, 0, Time.deltaTime));
        }
        else
        {
            light2d.pointLightInnerRadius = 0;
            UIManager.Instance.lightBar.CurValue = light2d.pointLightInnerRadius;
        }

        // 게이지 빼기
        

        // 각도 조절하는 것까지 
        light2d.pointLightInnerAngle = Mathf.Lerp(light2d.pointLightInnerAngle, TagetAngleInner, Time.deltaTime);
        light2d.pointLightOuterAngle = light2d.pointLightInnerAngle;

        float lightRadius = light2d.pointLightInnerRadius;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(light2d.transform.position, lightRadius);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    private void AttackLight()
    {
        // 점점 세개 만드는 것을 의미합니다.
        light2d.pointLightInnerRadius = Mathf.Lerp(light2d.pointLightInnerRadius, light2d.pointLightOuterRadius, 1.5f * Time.deltaTime);

        light2d.pointLightOuterAngle = Mathf.Lerp(light2d.pointLightOuterAngle, AttackAngelOuter, 1.5f * Time.deltaTime);

        UIManager.Instance.lightBar.Add(light2d.pointLightInnerRadius);

        if (light2d.pointLightInnerAngle > light2d.pointLightOuterAngle)
        {
            light2d.pointLightInnerAngle = light2d.pointLightOuterAngle;
        }

        

        float lightRadius = light2d.pointLightInnerRadius;
        //레이어 만드는 것이면 그것 충돌 체크 하기위해서 하는 것고
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(light2d.transform.position, lightRadius);
        

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Enemy"))
            {
                //여기서 적 체력관리하게끔 만든다.
                if(collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    currentEnemies.Add(enemy);
                    if (enemy.isAwaken)
                    {
                        enemy.EnemyHit(1f);
                    }
                    else
                    {
                        enemy.isAwaken = true;
                    }
                }
            }
        }
        // 범위에서 벗어난 적 찾기
        foreach (Enemy enemy in previousEnemies)
        {
            if (!currentEnemies.Contains(enemy))
            {
                enemy.EnemyHitEnd();
            }
        }

        // 이전 프레임의 적 목록을 현재 프레임의 적 목록으로 갱신
        previousEnemies = currentEnemies;
    }
}
