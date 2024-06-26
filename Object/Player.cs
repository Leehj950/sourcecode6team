using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    public PlayerController Controller { get { return controller; } }

    [SerializeField] private float interactRange = 0.6f;
    private void Awake()
    {
        GameManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }

    public void PressUse()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, interactRange);
        
        if(collider2D.TryGetComponent<IInteract>(out IInteract interact))
        {
            interact.Interact();
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }



    public void PlayerHit(float health, float damage)
    {
        // 체력 down
        health = health - damage > 0 ? health - damage : 0;

    }
}

