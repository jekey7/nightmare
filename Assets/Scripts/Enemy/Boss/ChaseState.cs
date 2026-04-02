using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IState
{
    private NavMeshAgent agent; // 에이전트
    private GameObject player; // 플레이어
    private Animator animator; // 애니메이터

    [Header("Enemy Settings")]
    [SerializeField] private float moveSpeed = 5.5f; // 이동 속도

    public void EnterState()
    {
        // 참조 초기화
        if (!animator) animator = transform.GetComponentInChildren<Animator>();
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!player) player = GameObject.Find("Player");

        // 애니메이션 초기화
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", true);

        // 이동 속도 초기화
        agent.speed = moveSpeed;
    }

    public void UpdateState()
    {
        // 게임 종료 시 추적 종료
        if (GameManager.Instance.isGameOver)
        {
            // 이동속도 0으로 초기화 및 에이전트 중지 (2중 방어)
            agent.speed = 0f;
            agent.isStopped = true;
            return; 
        }

        Movement(); // 이동 처리
    }

    // 이동 처리
    private void Movement()
    {
        // 에이전트를 통해 플레이어 위치로 자동 이동
        agent.SetDestination(player.transform.position);
    }

    public void ExitState()
    {
        // 애니메이션 초기화
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
    }
}
