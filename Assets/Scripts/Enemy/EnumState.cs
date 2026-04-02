// 보스의 상태
public enum BossEnumState
{
    Idle, // 대기
    Chase, // 추적
    Attack, // 공격
    Teleport, // 텔레포트
    Die, // 사망
}

// 일반 적의 상태
public enum EnemyEnumState
{
    // 스폰하자 마자 추적하므로 대기 상태 없음
    Chase, // 추적
    Attack, // 공격
    Die, // 사망
}