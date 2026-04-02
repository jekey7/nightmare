using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraRoot;

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("Camera Pitch Limits")]
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 30f;

    private float currentAimX, currentAimY;

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        CameraInput();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        HandleBodyRotation();
        HandleCameraRotation();
    }

    private void CameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        currentAimX += mouseX;
        currentAimY -= mouseY;
    }

    private void HandleBodyRotation()
    {
        transform.localRotation = Quaternion.Euler(0, currentAimX, 0);
    }

    private void HandleCameraRotation()
    {
        currentAimY = Mathf.Clamp(currentAimY, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(currentAimY, currentAimX, 0f);
    }
}
