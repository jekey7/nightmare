public class EnemyStateContext
{
    // 현재 상태
    // 인터페이스를 통해 상태 관리
    public IState currentState { get; set; } 

    private readonly EnemyAI _enemyAI; // EnemyAI 참조

    public EnemyStateContext(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI; // EnemyAI 초기화
    }

    public void Transition(IState state)
    {
        if (currentState != null)
            currentState.ExitState(); // 현재 상태 종료(초기화)

        currentState = state; // 새로운 상태로 전환

        currentState.EnterState(); // 새로운 상태 진입
    }
}
