using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IState
{
    private GameObject player; // 플레이어 참조

    public void EnterState()
    {
        if (!player) player = GameObject.Find("Player"); // 플레이어 오브젝트 Find

        Attack(); // 공격 실행
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
        player.GetComponent<PlayerHealth>().PlayerDie("Mut"); // 플레이어 사망 처리
    }
}
