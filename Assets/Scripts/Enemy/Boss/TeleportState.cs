using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportState : MonoBehaviour, IState
{
    [Header("Ref")]
    [SerializeField] private List<Transform> teleportSpot; // 텔레포트 위치 리스트
    [SerializeField] private GameObject SmokeEffect; // 텔레포트 스모크

    public event Action OnTeleported; // 텔레포트 이벤트

    private Animator animator; // 애니메이터
    private NavMeshAgent agent; // 에이전트

    public void EnterState()
    {
        // 참조 초기화
        if (!animator) animator = transform.GetComponentInChildren<Animator>();
        if (!agent) agent = GetComponent<NavMeshAgent>();

        // 애니메이션 초기화
        animator.SetBool("isIdle", true);
        animator.SetBool("isChasing", false);

        InstantTeleportEffect(); // 텔레포트 스모크 소환
        TeleportToSpot(); // 텔레포트 실행
    }

    public void UpdateState()
    {

    }

    // 텔레포트 처리
    private void TeleportToSpot()
    {
        int randomIndex = UnityEngine.Random.Range(0, teleportSpot.Count); // 랜덤 위치

        Vector3 spot = teleportSpot[randomIndex].position; // 랜덤 지정된 스팟 벡터값 저장
        agent.Warp(spot); // 해당 위치로 워프

        OnTeleported?.Invoke(); // 텔레포트 했음을 알림
    }

    // 텔레포트 스모크 생성
    private void InstantTeleportEffect()
    {
        GameObject smoke = Instantiate(SmokeEffect, transform.position, Quaternion.identity); // 스모크 생성
        smoke.transform.rotation = Quaternion.Euler(-120f, 0f, 0f); // 스모크 회전 초기화
        
        Destroy(smoke, smoke.GetComponent<ParticleSystem>().main.duration); // 파티클 지속시간이 끝났다면 오브젝트 파괴
    }

    public void ExitState()
    {
        // 애니메이션 초기화
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
    }
}
