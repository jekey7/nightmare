using UnityEngine;
using UnityEngine.Playables;

public class CineSignal : MonoBehaviour
{
    [SerializeField] private float probability = 20f; // 팝 아웃 확률

    private PlayableDirector playableDirector; // PD

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>(); // PD 참조
    }

    // 팝 아웃 처리
    public void BrunchPopOut()
    {
        int randomIndex = Random.Range(0, 100); // n/100 확률

        if (randomIndex > probability) // 당첨이 아니라면
        {
            playableDirector.time = 0; // 시네머신 초기화
            playableDirector.Play(); // 시너메신 실행
        }
    }
}
