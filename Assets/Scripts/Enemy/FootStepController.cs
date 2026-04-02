using UnityEngine;

public class FootStepController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource audioSource; // 오디오 소스
    [SerializeField] private AudioClip footStepClip; // 발소리 클립

    private void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>(); // 오디오 소스 참조
    }

    // 애니메이션의 이벤트와 연동되어 발소리 재생
    public void OnFootStep()
    {
        audioSource.PlayOneShot(footStepClip); // 발소리 재생
    }
}
