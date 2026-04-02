using UnityEngine;

public class SignalController : MonoBehaviour
{
    // 인게임 씬 프리로드
    public void OnPreloadIngame()
    {
        SceneLoadController.Instance.PreloadScene("IngameScene");
    }

    // 보스 씬 프리로드
    public void OnPreloadBoss()
    {
        SceneLoadController.Instance.PreloadScene("BossScene");
    }

    // 프리로드 된 씬 활성화
    public void OnActivateIngame()
    {
        SceneLoadController.Instance.ActivateLoadedScene();
    }
}
