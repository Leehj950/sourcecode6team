using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /// <summary>
    /// Value
    /// </summary>
    private PlayerController controller;
    private Rigidbody2D moverigidbody2D;
    private Vector2 moveVec;

    ///.... 나중에 정리 해야할 것들 

    // 사운드 매니저로 
    public float speed = 1.5f;
    public float runSpeed = 2.2f; // 달리기 속도
    public float walkSoundPitch = 1f; // 걷는 소리 기본 피치
    public float runSoundPitch = 1.5f; // 달리기 소리 피치

    // 이것은 사운드 매니저
    public AudioClip walkingSound; // 추가
    private AudioSource audioSource; // 추가

    // 애니메이션
    private Animator animator;
    private bool isRunning = false;
    
    //HealSystem;
    public float maxStamina = 100f; // 최대 체력
    private float currentStamina; // 현재 체력
    public float dashStaminaConsumption = 20f; // 초당 대시 체력 소모
    public float staminaRecoveryRate = 10f; // 초당 체력 회복
    private float lastDashTime = 0f; // 마지막 대시 시간이 0이 된 시간
    private bool canDash = true;
    private HealthBar healthBar;

    ///....

    /// <summary>
    /// Method
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        moverigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // 추가
        animator = GetComponent<Animator>(); // 추가
        currentStamina = maxStamina;

        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("HealthBar not found in the scene!");
        }
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    // 프레임다운 마다 연산된 것 업데이트를 해줘야하니까.

    private void Update()
    {
        // 리펙토링 해야한다.
        bool wantToRun = GameManager.Instance.Player.Controller.IsRuning && currentStamina > 0; // 변경: 달리기 입력 상태
        isRunning = wantToRun && canDash; // 변경: 달리기 가능 여부 조건

        ApplyMoveMent(moveVec);

        bool IsWalking = moveVec != Vector2.zero;
        animator.SetBool("IsWalking", IsWalking);
        animator.speed = isRunning && IsWalking ? 1.5f : 1f; // 애니메이션 속도 설정

        if (!canDash && Time.time - lastDashTime >= 2f)
        {
            canDash = true;
        }

        // SHIFT를 누르고 있으면서 대시 가능하고 체력이 모자라면 달리기 중지
        isRunning = GameManager.Instance.Player.Controller.IsRuning && currentStamina > 0 && canDash;

        if (!canDash && Time.time - lastDashTime >= 2f)
        {
            canDash = true;
        }

        if (isRunning && !wantToRun)
        {
            isRunning = false;
        }

        // UI 업데이트: 체력 슬라이더 업데이트
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentStamina, maxStamina);
        }
    }
    private void FixedUpdate()
    {
        if (isRunning && currentStamina > 0) // 체력이 모자라면 달리기 중지
        {
            ConsumeStamina(dashStaminaConsumption * Time.fixedDeltaTime);
        }
        else
        {
            RecoverStamina(staminaRecoveryRate * Time.fixedDeltaTime);
        }
    }

    private void ApplyMoveMent(Vector2 vec2)
    {
        // 이제 아이소메트릭 맵에 대한 것에서 계산을 합니다.
        // 아래와 같이 코드를 짜면 방향키 두키를 눌렀을때 이게 문제 발생할수있다.

        //버전1.2
        // 동시에 눌렀을 때.  8방향만 움직이겠끔
        if ((vec2.x == 1 || vec2.x == -1) && vec2.y == 1)
        {
            vec2.y = 0.5f;
        }

        else if ((vec2.x == 1 || vec2.x == -1) && vec2.y == -1)
        {
            vec2.y = -0.5f;
        }

        float currentSpeed = isRunning ? runSpeed : speed;

        moverigidbody2D.velocity = vec2 * currentSpeed;

        //애니메이션으로 가도 된다.
        if (vec2 != Vector2.zero) // 추가
        {
            PlayWalkingSound(); // 추가
            animator.SetBool("IsWalking", true); // 추가
        }
        else // 추가
        {
            StopWalkingSound(); // 추가
            animator.SetBool("IsWalking", false); // 추가
        }
    }

    // 사운드 매니저로 가자.
    private void PlayWalkingSound() // 추가
    {
        if (!audioSource.isPlaying) // 추가
        {
            audioSource.clip = walkingSound; // 추가
            audioSource.loop = true; // 추가
            audioSource.Play(); // 추가
        }
        audioSource.pitch = isRunning ? runSoundPitch : walkSoundPitch; 
    }

    // 사운드 매니저로..
    private void StopWalkingSound() // 추가
    {
        if (audioSource.isPlaying) // 추가
        {
            audioSource.Stop(); // 추가
        }
    }

    private void Move(Vector2 MoveVec)
    {
        moveVec = MoveVec;
    }
    
    // Healsystem 관리하는 스태미너로가야한다.
    private void ConsumeStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0;
            canDash = false; // 대시 불가능 상태로 변경
            lastDashTime = Time.time; // 마지막 대시 시간 업데이트
        }
    }

    private void RecoverStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }
}

