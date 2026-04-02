using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [Header("Object Settings")]
    [SerializeField] private GameObject[] spawnPoints; // 스폰 지점들
    [SerializeField] private GameObject mut; // 멋
    [SerializeField] private GameObject fadeOutEffect; // 보스 씬 전환을 위한 페이드 아웃 UI

    [Header("Spawn Settings")]
    [SerializeField] private int targetCount = 20; // 목표 개수
    [SerializeField] private float spawnInterval = 5.0f; // 스폰 간격

    private float spawnTimer = 0.0f; // 스폰 타이머
    [SerializeField] private int currentCount = 0; // 현재 스폰된 개수
    [SerializeField] private int survivedCount = 0; // 생존한 개수

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        TimeTick(); // 시간 경과 처리
        EnemySpawn(); // 적 스폰 처리

        // 라운드 종료 체크
        // 현재 스폰된 적 수가 목표치 이상이고 생존한 적 수가 0 이하일 경우
        if (currentCount >= targetCount && survivedCount <= 0f)
            RoundOver(); // 라운드 종료 처리
    }
    private void TimeTick()
    {
        // 현재 시간 += 델타 타임
        spawnTimer += Time.deltaTime;
        // 스폰 시간 최대치 넘지 않도록 제한
        if (spawnTimer > spawnInterval)
            spawnTimer = spawnInterval;
    }

    // 적 스폰 처리
    private void EnemySpawn() 
    {
        // 스폰 시간 전 & 현재 스폰된 미믹 수가 목표치 이상일 경우 리턴
        if (spawnTimer < spawnInterval) return;
        if (currentCount >= targetCount) return;

        int rndIndex = Random.Range(0, spawnPoints.Length); // 랜덤 위치
        GameObject Enemy = Instantiate(mut, spawnPoints[rndIndex].transform.position, Quaternion.identity); // 적 스폰
        Enemy.GetComponent<Health>().OnDied += EnemyDied; // 적 사망 이벤트 구독

        currentCount++; // 현재 스폰된 적 수 증가
        survivedCount++; // 생존한 적 수 증가
        spawnTimer = 0.0f; // 타이머 초기화
    }

    // 적 사망 처리
    public void EnemyDied(Health health)
    {
        health.OnDied -= EnemyDied; // 적 사망 이벤트 구독 해제
        survivedCount--; // 생존한 미믹 수 감소
    }

    // 라운드 종료 체크
    private void RoundOver()
    {
        fadeOutEffect.SetActive(true); // 씬 전환을 위한 페이드 아웃 실행
    }
}
