using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class Condition : MonoBehaviour
{
    public float CurValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        CurValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        CurValue = Mathf.Min(CurValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        CurValue = Mathf.Max(CurValue - amount, 0);
    }

    private float GetPercentage()
    {
        return CurValue / maxValue;
    }
}
