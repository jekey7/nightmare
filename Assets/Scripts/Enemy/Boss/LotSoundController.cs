using UnityEngine;

public class LotSoundController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource audioSource; // 오디오 소스
    [SerializeField] private AudioClip[] growlClip; // 울음소리 클립들

    // 울음소리 출력
    public void PlayGrowl(int index)
    {
        // 페이즈에 따라 울음소리 다르게 출력
        audioSource.PlayOneShot(growlClip[index - 1]);
    }
}
