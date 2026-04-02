// 적과 보스의 상태 인터페이스
public interface IState
{
    public void EnterState(); // 상태 진입 시 호출
    public void UpdateState(); // 매 프레임 상태 업데이트 시 호출
    public void ExitState(); // 상태 종료 시 호출 (초기화 용)
}
