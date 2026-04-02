// Supported by GENINI

using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerShooter shooter; // 플레이어 슈터

    [Header("Low Ready Sway Settings")]
    [SerializeField] private float hipAmount = 0.1f; // 총이 밀리는 정도 (클수록 많이 밀림)
    [SerializeField] private float maxHipAmount = 5f; // 총이 밀리는 최대 범위 (너무 많이 밀리지 않게 제한)
    [SerializeField] private float smoothHipAmount = 6f; // 돌아오는 속도 (클수록 빠릿하고, 작을수록 묵직함)

    [Header("Aiming Sway Settings")]
    [SerializeField] private float aimAmount = 0.5f; // 총이 밀리는 정도 (클수록 많이 밀림)
    [SerializeField] private float maxAimAmount = 8f; // 총이 밀리는 최대 범위 (너무 많이 밀리지 않게 제한)
    [SerializeField] private float smoothAimAmount = 2f; // 돌아오는 속도 (클수록 빠릿하고, 작을수록 묵직함)

    private Vector3 initialPosition; // 총의 원래 위치 (중앙)
    private float movementX, movementY; // 마우스 X, Y

    private void Start()
    {
        SaveInitialPos(); // 시작 위치 초기화
    }

    private void SaveInitialPos()
    {
        // 시작 시 원래 위치를 기억해둡니다.
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        InputSway(); // 흔들림 처리를 위한 마우스 입력 처리
    }

    private void LateUpdate()
    {
        HandleSway(); // 흔들림 처리
    }

    private void InputSway()
    {
        // 조준 여부에 따라 Sway 정도 조정
        float amount = shooter.isAiming ? aimAmount : hipAmount;
        float maxAmount = shooter.isAiming ? maxAimAmount : maxHipAmount;

        // 마우스 입력
        movementX = -Input.GetAxis("Mouse X") * amount; // 마우스 방향과 반대로 움직여야 하므로 (-)를 붙임
        movementY = -Input.GetAxis("Mouse Y") * amount;

        // 이동할 범위를 제한합
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);
    }

    private void HandleSway()
    {
        // 목표 위치 설정
        Vector3 finalPosition = new Vector3(movementX, movementY, 0);

        float smoothAmount = shooter.isAiming ? smoothAimAmount : smoothHipAmount;

        // 현재 위치에서 목표 위치로 부드럽게 이동(Lerp)
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
