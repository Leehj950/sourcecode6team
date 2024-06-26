using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AltarRoom : MonoBehaviour
{
    bool isTriggered = false;
    public Enemy enemy;
    public GameObject dummyEnemy;

    public Light2D[] lights;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isTriggered && collision.CompareTag("Player") )
        {
            isTriggered = true;
            StartCoroutine(EventAltarRoom());

        }
    }


    IEnumerator EventAltarRoom()
    {
        // 사운드 출력
        // 방 불이 꺼지듯이 어두워졌다 밝아진다. 그 사이에 서있는 에네미로 교체

        foreach(var light in lights)
        {
            Color color = light.color;
            color.a = 0;
            light.color = color;            
        }
        dummyEnemy.SetActive(false);
        enemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        foreach (var light in lights)
        {
            Color color = light.color;
            color.a = 1;
            light.color = color;
        }

        enemy.EnemyWakeUp();
    }
}
