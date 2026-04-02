// Supported by ChatGPT
// 코드 최적화 및 로직 수정

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
    public static SceneLoadController Instance; // 싱글톤

    private AsyncOperation currentOperation; // 현재 로딩 진행
    private bool isReadyToActivate = false; // 활성화 가능 여부

    private void Awake()
    {
        // 싱글톤 생성
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PreloadScene(string sceneName)
    {
        if (currentOperation != null) return; // 중복 방지
        StartCoroutine(LoadSceneAsync(sceneName)); // 프리로딩 실행
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        currentOperation = SceneManager.LoadSceneAsync(sceneName); // 로딩 시작
        currentOperation.allowSceneActivation = false; // 로딩 허용 전 로딩 불가

        // 90%까지 로딩 대기
        while (currentOperation.progress < 0.9f)
        {
            yield return null;
        }

        isReadyToActivate = true; // 이제 활성화 가능
    }

    public void ActivateLoadedScene()
    {
        if (currentOperation != null && isReadyToActivate)
        {
            GameManager.Instance.isGameOver = false;
            currentOperation.allowSceneActivation = true; // 로딩 허용
            currentOperation = null; // 진행 상태 초기화
            isReadyToActivate = false; // 허용 여부 초기화
        }
    }
}
