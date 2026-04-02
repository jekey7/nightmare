using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤
    private IScene scene; // 인터페이스로 씬 로더 참조

    public string NextSceneName { get; private set; } // 넘어갈 다음 씬 이름 저장

    public bool isGameOver { get; set; } = false; // 게임오버 여부

    private void Awake()
    {
        // 싱글톤 화(초기화)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            scene = GetComponent<IScene>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string targetSceneName)
    {
        NextSceneName = targetSceneName; // 목적지 저장

        if (scene != null)
        {
            scene.LoadScene("LoadScene"); // 로딩 씬 먼저 호출
        }
    }
}
