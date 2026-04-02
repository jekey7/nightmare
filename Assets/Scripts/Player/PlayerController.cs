using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 2f; // 이동 속도
    [SerializeField] private float gravity = -9.81f; // 중력 가속도

    private CharacterController characterController; // 캐릭터 컨트롤러 참조
    private Vector3 velocity; // 중력 속도

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // CC 참조
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        ApplyGravity(); // 중력 처리
        Movement(); // 이동 처리
    }

    private void Movement()
    {
        // 키 입력 받기
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical"); // W/S

        // 이동 방향 계산
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize(); // 대각선 이동 시 속도 보정

        // 이동 적용
        Vector3 vel = transform.TransformDirection(dir) * moveSpeed; // 이동 속도 계산
        characterController.Move(vel * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0f) // 바닥에 닿아있다면
        {
            // 바닥에 붙어있게 살짝 눌러줌
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime; // 중력 계산
        characterController.Move(velocity * Time.deltaTime); // 중력 적용
    }
}
