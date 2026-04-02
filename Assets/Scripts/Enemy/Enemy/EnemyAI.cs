using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("EnemyState References")]
    [SerializeField] private EnemyChaseState chaseState; // 추적 상태
    [SerializeField] private EnemyAttackState attackState; // 공격 상태
    [SerializeField] private EnemyDieState dieState; // 사망 상태
    [SerializeField] private Health enemyHealth; // 적 체력

    [Header("Enemy Settings")]
    [SerializeField] private float attackRange = 3f; // 공격 사거리

    private EnemyStateContext stateContext; // 상태 컨텍스트
    private bool isDead = false; // 사망 여부

    private void Awake()
    {
        stateContext = new EnemyStateContext(this); // 상태 컨텍스트 초기화
    }

    private void Start()
    {
        SwitchState(EnemyEnumState.Chase); // 시작: 추적 상태로 전환
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) gameObject.SetActive(false); // 게임 오버되면 그냥 비활성화
        if (isDead) return;

        CheckAttackRange(); // 공격 사거리 체크
        CheckEnemyHp(); // 체력 체크
        stateContext.currentState.UpdateState(); // Update 상태 호출
    }

    private void CheckAttackRange()
    {
        // 사거리 내에 플레이어 있다면 공격 || 없다면 추적
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= attackRange)
        {
            SwitchState(EnemyEnumState.Attack); // 추적 -> 공격
        }
    }

    private void SwitchState(EnemyEnumState state)
    {
        //상태 전환
        switch (state)
        {
            case EnemyEnumState.Chase:
                stateContext.Transition(chaseState);
                break;
            case EnemyEnumState.Attack:
                stateContext.Transition(attackState);
                break;
            case EnemyEnumState.Die:
                stateContext.Transition(dieState);
                break;
        }
    }

    private void CheckEnemyHp()
    {
        // 체력 0 이하 시 사망 상태로 전환
        if (enemyHealth.Hp <= 0)
        {
            isDead = true;
            SwitchState(EnemyEnumState.Die);
        }
    }
}
