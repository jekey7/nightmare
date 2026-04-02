using UnityEngine;
using UnityEngine.AI;

public class DieState : MonoBehaviour, IState
{
    [Header("Settings")]
    [SerializeField] private Animator animator; // 에니메이터
    [SerializeField] private NavMeshAgent agent; // 네브 메쉬 에이전트
    [SerializeField] private GameObject gasParticle; // 사망 가스
    [SerializeField] private GameObject fadeOutUI; // 엔딩 전환을 위한 페이드 아웃 화면

    private void Start()
    {
        // 초기화
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!agent) agent = GetComponent<NavMeshAgent>();
    }

    public void EnterState()
    {
        fadeOutUI.SetActive(true); // 화면 전환 On

        agent.isStopped = true; // 에이전트 스탑

        Instantiate(gasParticle, transform.position, Quaternion.identity); // 가스 생성
        animator.SetTrigger("Die"); // 사망 애니메이션 재생
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
