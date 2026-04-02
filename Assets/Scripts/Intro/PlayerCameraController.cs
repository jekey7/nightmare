using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin perlin; // 시네머신 카메라 흔들림(노이즈) 제어용 Perlin 컴포넌트

    private void Start()
    {
        // 현재 오브젝트에 붙은 CinemachineVirtualCamera에서
        // 노이즈(카메라 흔들림)를 담당하는 Perlin 컴포넌트를 가져옴
        perlin = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        // 카메라 흔들림 시작
        // intensity: 흔들림 강도
        // duration : 흔들림이 지속되는 시간
        StartCoroutine(Shake(5f, 2.25f));
    }

    private IEnumerator Shake(float intensity, float duration)
    {
        // 흔들림의 기본 세기(진폭)를 즉시 설정
        perlin.m_AmplitudeGain = intensity;

        float elapsedTime = 0f;

        // 지정한 duration 동안 흔들림을 점진적으로 변화
        while (elapsedTime < duration)
        {
            // 경과 시간 누적
            elapsedTime += Time.deltaTime;

            // 진행 비율을 0 ~ 1 범위로 정규화
            float lerpedTime = Mathf.Clamp01(elapsedTime / duration);

            // 흔들림 빈도를 점점 증가시켜
            // 처음에는 느리게, 시간이 지날수록 빠르게 흔들리도록 연출
            perlin.m_FrequencyGain = Mathf.Lerp(0f, intensity, lerpedTime);

            yield return null;
        }
    }
}
