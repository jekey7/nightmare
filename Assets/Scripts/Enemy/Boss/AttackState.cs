using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private GameObject player; // 플레이어

    public void EnterState()
    {
        if (!player) player = GameObject.Find("Player"); // 플레이어 참조

        Attack(); // 공격 처리
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
        
    }

    // 공격 처리
    private void Attack()
    {
        player.GetComponent<PlayerHealth>().PlayerDie("Lot"); // 플레이어 사망 처리
        gameObject.SetActive(false); // 이 오브젝트 비활성화
    }
}
