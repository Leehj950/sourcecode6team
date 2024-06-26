using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSensor : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask TestLayer;
    public List<GameObject> detectedEnemies = new List<GameObject>();

    public bool isFight;
    public bool isEnemy;

    private void Start()
    {
        isFight = false;
        isEnemy = false;
    }

    private void Update()
    {
        if (!isFight)
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        detectedEnemies.Clear();

        Vector2 position = transform.position;
        // 플레이어 위치에서 감지반경 내의 콜라이더 감지
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, detectionRadius, TestLayer);


        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("TestObj"))
            {
                detectedEnemies.Add(collider.gameObject);
                Debug.Log("Add");
            }
        }
        if (hitColliders.Length < 1)
        {
            isEnemy = false;
            return;
        }
        else
        {
            isEnemy = true;
        }
        // 감지된 적을 거리순으로 정렬
        detectedEnemies = detectedEnemies.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).ToList();

        // 감지된 적 처리 (여기서는 단순히 디버그 출력)
        foreach (GameObject enemy in detectedEnemies)
        {
            Debug.Log("Detected enemy: " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Scene 뷰에서 감지 반경을 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}