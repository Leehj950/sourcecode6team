using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar; // Unity Inspector에서 설정할 체력 슬라이더

    private void Update()
    {
        UpdateHealthUI();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    private void UpdateHealthUI()
    {
        // 이곳에서 추가적인 UI 업데이트 로직을 넣을 수 있습니다.
    }
}