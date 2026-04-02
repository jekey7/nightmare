using UnityEngine;

public class BobSystem : MonoBehaviour
{
    // 좌우(X축) 흔들림의 최대 이동량
    [Range(0.001f, 0.01f)]
    [SerializeField] private float xAmount = 0.002f;

    // 상하(Y축) 흔들림의 최대 이동량
    [Range(0.001f, 0.01f)]
    [SerializeField] private float yAmount = 0.002f;

    // 흔들림 주파수 (값이 클수록 빠르게 흔들림)
    [Range(1f, 30f)]
    [SerializeField] private float frequency = 10f;

    // 원위치로 돌아오거나 흔들림을 적용할 때의 부드러움
    [Range(10f, 100f)]
    [SerializeField] private float smooth = 10f;

    // 시작 시의 기준 위치(로컬 좌표)
    private Vector3 startPosition;

    private void Start()
    {
        // 오브젝트의 초기 로컬 위치를 저장
        // Head Bob 종료 시 이 위치로 되돌아감
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        // 플레이어 이동 여부에 따라 Head Bob 시작 여부 판단
        HeadBobTrigger();

        // 이동이 없을 때 카메라를 원래 위치로 복귀
        StopHeadBob();
    }

    private void HeadBobTrigger()
    {
        // 플레이어 입력 방향의 크기 계산
        // (대각선 이동도 고려하기 위해 magnitude 사용)
        float inputMagnitude =
            new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        // 입력이 있을 경우 Head Bob 적용
        if (inputMagnitude > 0)
            StartHeadBob();
    }

    private void StartHeadBob()
    {
        // 이번 프레임에 적용할 이동값
        Vector3 pos = Vector3.zero;

        // 상하 흔들림 (Sin 파형)
        // yAmount로 흔들림 크기 조절
        // frequency로 흔들림 속도 조절
        pos.y += Mathf.Lerp(
            pos.y,
            Mathf.Sin(Time.time * frequency) * yAmount * 1.4f,
            smooth * Time.deltaTime
        );

        // 좌우 흔들림 (Cos 파형)
        // Y축보다 느리게 흔들리도록 frequency / 2 적용
        pos.x += Mathf.Lerp(
            pos.x,
            Mathf.Cos(Time.time * frequency / 2f) * xAmount * 1.6f,
            smooth * Time.deltaTime
        );

        // 계산된 흔들림 값을 현재 로컬 위치에 누적 적용
        transform.localPosition += pos;
    }

    private void StopHeadBob()
    {
        // 이미 기준 위치에 도달했다면 추가 처리하지 않음
        if (transform.localPosition == startPosition) return;

        // 이동 입력이 없을 때
        // 현재 위치에서 시작 위치로 부드럽게 복귀
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            startPosition,
            Time.deltaTime
        );
    }
}
