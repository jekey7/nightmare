using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingInLoadScene : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider loadingBar; // 로딩바

    private void Start()
    {
        // GameManager에 저장된 다음 씬 정보를 가져와서 로딩 시작
        if (GameManager.Instance != null && !string.IsNullOrEmpty(GameManager.Instance.NextSceneName))
        {
            StartCoroutine(LoadSceneProgress(GameManager.Instance.NextSceneName));
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    IEnumerator LoadSceneProgress(string sceneName)
    {
        // 비동기 로드 시작
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        loadingBar.value = 0f;

        // 씬 전환 불가(빠른 전환 방어)
        op.allowSceneActivation = false;

        float timer = 0f;

        // 로딩이 완료 될 때까지 루프
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            print(op.progress);

            // 90%에서 대기
            if (op.progress < 0.90f)
            {
                // 자연스러운 로딩바 진행
                loadingBar.value = Mathf.Lerp(loadingBar.value, op.progress, timer);
                if (loadingBar.value >= op.progress)
                    timer = 0f;
            }
            else
            {
                // 로딩 종료. 100%까지 진행
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1f, timer);

                // 씬 전환 허용
                if (loadingBar.value >= 0.99f)
                {
                    GameManager.Instance.isGameOver = false;
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
