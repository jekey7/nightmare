using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EndingSignal : MonoBehaviour
{
    [SerializeField] private ScriptableRendererFeature fogRenderer; // 안개 쉐이더 렌더러

    // 안개 off
    public void DisableFog()
    {
        fogRenderer.SetActive(false);
    }

    // 타이틀 씬 프리로드
    public void OnPreloadTitle()
    {
        SceneLoadController.Instance.PreloadScene("TitleScene");
    }

    // 씬 활성화
    public void OnActivateScene()
    {
        SceneLoadController.Instance.ActivateLoadedScene();
    }
}
