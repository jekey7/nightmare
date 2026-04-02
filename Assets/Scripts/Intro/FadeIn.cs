using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FadeIn : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Volume camPPVolume; // 카메라 후처리 볼륨

    [SerializeField] private float fadeInDuration = 2.6f; // 페이드 인 지속 시간
    [SerializeField] private float targetVignetteIntensity = 0.44f; // 목표 비네트 강도

    private void Start()
    {
        if (camPPVolume.profile.TryGet(out Vignette vignette)) // 비네트 참조 성공했다면
        {
            vignette.intensity.value = 1f; // 비네트 강도 1로 설정
        }
    }

    public void StartFadeIn()
    {
        StartCoroutine(CoFadeIn()); // 페이드 인 실행
    }

    // 페이드 인 처리
    private IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // 현재 시간

        // 페이드 인이 끝날 때 까지
        while(elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime; // 현재 시간에 deltaTime 누적
            float lerpedTime = Mathf.Clamp01(elapsedTime / fadeInDuration); // 보간 시간 계산
            if (camPPVolume.profile.TryGet(out Vignette vignette)) // 비네트 참조 성공했다면
            {
                // 비네트를 목표 강도까지 Lerp처리(부드럽게 페이드 인)
                vignette.intensity.value = Mathf.Lerp(1f, targetVignetteIntensity, lerpedTime);
            }
            yield return null;
        }
    }
}
