using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : MonoBehaviour, IState
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.5f; // 미믹 이동 속도

    // 참조 변수
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    public void EnterState()
    {
        // 참조 초기화
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!player) player = GameObject.Find("Player");
        if (!animator) animator = GetComponent<Animator>();

        agent.speed = moveSpeed; // 미믹 이동 속도 설정
    }

    public void UpdateState()
    {
        Movement(); // 이동 처리
    }

    private void Movement()
    {
        animator.SetBool("isChasing", true); // 애니메이션 전환
        agent.SetDestination(player.transform.position); // 미믹 이동
    }

    public void ExitState() 
    {
        animator.SetBool("isChasing", false); // 애니메이션 전환
    }
}
