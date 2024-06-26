using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isAwaken = false;
    public bool isHit = false;
    public bool isDead = false;

    public float enemyHealth = 100f;

    public GameObject target;
    public float speed = 4f;
    public float movetime = 0.5f;
    public float interval = 1.5f;
    public Player player;
    public Animator animator;
    public AudioSource audioSource;

    IEnumerator myCoroutine;

    private void Start()
    {
        player = GameManager.Instance.Player;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (enemyHealth <= 0f && !isDead)
        {
            EnemyDead();
        }
    }

    public IEnumerator EnemyMove()
    {
        while (isAwaken && !isHit)
        {
            Vector2 dirToPlayer = target.transform.position - transform.position;

            // 방향에 따라 캐릭터를 좌우 반전시킴
            if (dirToPlayer.x < 0)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 1); // 좌측
            }
            else
            {
                transform.localScale = new Vector3(-0.4f, 0.4f, 1); // 우측
            }

            rb.velocity = dirToPlayer.normalized * speed;
            animator.SetBool("isRunning", true);
            yield return null;
        }
    }

    public void EnemyWakeUp()
    {
        isAwaken = true;
        target = player.gameObject;
        audioSource.Play();
        myCoroutine = EnemyMove();
        StartCoroutine(myCoroutine);
        
    }

    public void EnemyHit(float attackDamage)
    {
        isHit = true;
        enemyHealth -= attackDamage;
        // TODO :: 피격 애니메이션 또는 파티클효과
    }

    public void EnemyHitEnd()
    {
        isHit = false;
    }

    public void EnemyDead()
    {
        StopCoroutine(myCoroutine);
        isAwaken = false;
        isDead = true;
        // TODO :: 쓰러지는 애니메이션, 처리
        // z 90도 돌리기
        transform.Rotate(0, 0, 90f);
        animator.SetBool("isRunning", false);
        rb.velocity = Vector2.zero;
        audioSource.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            //player.PlayerHit();
        }
    }
}