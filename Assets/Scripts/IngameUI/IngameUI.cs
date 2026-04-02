using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IngameUI : MonoBehaviour
{
    [Header("Post-Processing Settings")]
    [SerializeField] private Volume pp; // 후처리 볼륨

    private void OnEnable()
    {
        Bloom bloom; // 블룸 효과
        pp.profile.TryGet(out bloom); // 블룸 효과 참조
        bloom.scatter.Override(0.4f); // 블룸 스카터 0.4f
    }

    private void OnDisable()
    {
        Bloom bloom; // 블룸
        pp.profile.TryGet(out bloom); // 참조
        bloom.scatter.Override(1f); // 블룸 스카터 1f(초기화)
    }

    // Restart 버튼 용
    public void OnLoadIngame()
    {
        GameManager.Instance.LoadScene("IngameScene");
    }

    // Title 버튼 용
    public void OnLoadTitle()
    {
        GameManager.Instance.LoadScene("TitleScene");
    }

    // Restart 버튼 용
    public void OnLoadBoss()
    {
        GameManager.Instance.LoadScene("BossScene");
    }

    // 보스 인트로 프리로딩
    public void OnPreLoadBoss()
    {
        SceneLoadController.Instance.PreloadScene("BossIntroScene");
    }

    // 엔딩 씬 프리로딩
    public void OnPreLoadEnding()
    {
        SceneLoadController.Instance.PreloadScene("EndScene");
    }

    // 프리로딩 활성화 허용
    public void OnActivatePreloadingScene()
    {
        SceneLoadController.Instance.ActivateLoadedScene();
    }
}
