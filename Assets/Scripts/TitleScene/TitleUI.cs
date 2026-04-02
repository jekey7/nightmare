// Supported by Gemini
// Gemini의 코드 최적화 도움 (비효율적인 로직 단순화)

using DG.Tweening; // 닷트윈 사용을 위한 네임스페이스
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup titleUI; // 타이틀 UI 캔버스 그룹
    [SerializeField] private Image fadePanel; // 페이드 아웃 연출을 취한 패널
    [SerializeField] private ScriptableRendererFeature fogRenderer; // 안개 활성화(초기화)를 위한 참조

    private void Start()
    {
        // UI 초기화
        titleUI.alpha = 0f; // 타이틀 UI 투명 상태에서 시작
        fadePanel.gameObject.SetActive(false); // 페이드 연출 패널 비활성화
        fogRenderer.SetActive(true); // 안개 쉐이더 활성화

        titleUI.DOFade(1f, 7f).SetEase(Ease.OutQuad); // 타이틀 UI 페이드 인
    }

    // Start 버튼 클릭 이벤트 처리
    public void OnPressStart()
    {
        titleUI.blocksRaycasts = false; // UI 입력 블락 처리
        SceneLoadController.Instance.PreloadScene("IntroScene");

        fadePanel.gameObject.SetActive(true); // 페이드 연출 패널 활성화
        fadePanel.color = new Color(0, 0, 0, 0); // Alpha 값 0으로 초기화

        // 페이드 아웃 연출 실행
        fadePanel.DOFade(1f, 3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 페이드 아웃이 완료 되었다면 인트로 씬 로드
                SceneLoadController.Instance.ActivateLoadedScene();
            });
    }

    public void OnPressQuit()
    {
#if UNITY_EDITOR // 지금 에디터라면
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 종료
#else // 빌드 게임이라면
        Application.Quit(); // 어플 종료
#endif
    }
}
