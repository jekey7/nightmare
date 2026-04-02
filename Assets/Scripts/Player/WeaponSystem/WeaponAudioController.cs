using UnityEngine;

public class WeaponAudioController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource audioSource; // 오디오 소스
    [SerializeField] private AudioClip removeMagClip; // 탄창 제거 클립
    [SerializeField] private AudioClip insertMagClip; // 탄창 삽입 클립
    [SerializeField] private AudioClip slideCockClip; // 슬라이드 콕킹 클립

    private void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>(); // 오디오 소스 참조
    }

    // 아래 모두 애니메이션 이벤트를 통해 재생 처리

    // 탄창 제거
    public void OnRemoveMag()
    {
        if (removeMagClip != null) audioSource.PlayOneShot(removeMagClip);
    }

    // 탄창 삽입
    public void OnInsertMag()
    {
        if (insertMagClip != null) audioSource.PlayOneShot(insertMagClip);
    }

    // 슬라이더 콕킹
    public void OnSlideCock()
    {
        if (slideCockClip != null) audioSource.PlayOneShot(slideCockClip);
    }
}
