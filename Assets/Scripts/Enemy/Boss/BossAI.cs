using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("BossState Reference")] // 보스 상태 참조
    [SerializeField] private IdleState idleState; // 대기 상태
    [SerializeField] private ChaseState chaseState; // 추적 상태
    [SerializeField] private AttackState attackState; // 공격 상태
    [SerializeField] private TeleportState teleportState; // 텔레포트 상태
    [SerializeField] private DieState dieState; // 사망 상태
    [SerializeField] private Health bossHealth; //보스 체력
    [SerializeField] private LotSoundController sound; // 보스 사운드

    [Header("Enemy Settings")]
    [SerializeField] private float attackRange = 3f; // 공격 사거리

    private BossStateContext stateContext; // 상태 컨텍스트
    private int phase = 1; // 페이즈 (체력 분기 및 명시적 페이즈 관리용)
    private bool isDead = false; // 사망 여부

    private void Awake()
    {
        stateContext = new BossStateContext(this); // 상태 컨텍스트 초기화
        SwitchState(BossEnumState.Idle); // 초기 상태 설정 (대기)
    }

    private void OnEnable()
    {
        // OnTeleported에 추적 상태 구독
        // 텔레포트 후 추적 상태로 전환
        teleportState.OnTeleported += () => SwitchState(BossEnumState.Chase);
    }

    private void OnDisable()
    {
        // 이벤트 구독 해제
        teleportState.OnTeleported -= () => SwitchState(BossEnumState.Chase);
    }

    private void Start()
    {
        // 게임 시작부터 텔레포트 -> 이후 추적
        SwitchState(BossEnumState.Teleport);
        sound.PlayGrowl(phase); // 울음소리 재생
    }

    private void Update()
    {
        if (isDead) return; // 사망 시 업데이트 중지
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        CheckAttackRange(); // 공격 사거리 체크
        BranchHP(); // 체력 분기 처리
        stateContext.currentState.UpdateState(); // Update 상태 호출
    }

    private void CheckAttackRange()
    {
        // 사거리 내에 플레이어 있다면 공격 || 없다면 추적
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= attackRange)
        {
            SwitchState(BossEnumState.Attack);
        }
    }

    private void SwitchState(BossEnumState state)
    {
        // 상태 전환 처리
        switch (state)
        {
            case BossEnumState.Idle:
                stateContext.Transition(idleState);
                break;
            case BossEnumState.Chase:
                stateContext.Transition(chaseState);
                break;
            case BossEnumState.Attack:
                stateContext.Transition(attackState);
                break;
            case BossEnumState.Teleport:
                stateContext.Transition(teleportState);
                break;
            case BossEnumState.Die:
                stateContext.Transition(dieState);
                break;
        }
    }

    private void BranchHP()
    {
        // 체력 0 이하 시 사망 상태로 전환
        if (bossHealth.Hp <= 0f) 
        {
            print("Die!");
            SwitchState(BossEnumState.Die); // 사망 상태로 전환
            isDead = true; // 사망 처리
        }
        // 페이즈별 체력 체크 및 상태 전환
        else if (bossHealth.Hp <= bossHealth.InitialHp * ((5 - phase) / 5f))
        {
            phase++; // 페이즈 증가
            sound.PlayGrowl(phase); // 울음소리 재생
            SwitchState(BossEnumState.Teleport); // 텔레포트 상태로 전환
        }
    }
}
