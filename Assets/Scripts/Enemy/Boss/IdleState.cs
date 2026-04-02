using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    private Animator animator; // 애니메이터

    public void EnterState()
    {
        if(!animator) animator = transform.GetComponentInChildren<Animator>(); // 자식에 있는 애니메이터 참조

        // 애니메이션 초기화
        animator.SetBool("isIdle", true); // 대기 애니메이션 실행
        animator.SetBool("isChasing", false); // 추적 애니메이션 비활성화
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
        // 애니메이션 초기화
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
    }
}
