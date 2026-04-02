using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDieState : MonoBehaviour, IState
{
    [Header("References")]
    [SerializeField] private Animator animator; // 애니메이터
    [SerializeField] private NavMeshAgent agent; // 에이전트

    public void EnterState()
    {
        StartCoroutine(DieEnemy()); // 적 사망 처리
    }

    public void UpdateState() { }

    public void ExitState() { }

    private IEnumerator DieEnemy()
    {
        agent.isStopped = true;
        animator.SetTrigger("Die"); // 사망 애니메이션 재생
        transform.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2.5f); // 애니메이션 재생 대기
        Destroy(gameObject);
    }
}
