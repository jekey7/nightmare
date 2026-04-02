using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverUI; // 게임 오버 UI
    [SerializeField] private GameObject mut; // Mut_JumpScare_Root
    [SerializeField] private GameObject lot; // Lot_JumpScare_Root

    [Header("Settings")]
    [SerializeField] private float scareDuration = 2.0f; // 점프 스케어 지속 시간

    private void Start()
    {
        // 초기화
        gameOverUI.SetActive(false); // 게임 오버 UI 비활성화
        mut.SetActive(false); // 멋 비활성화
        lot.SetActive(false); // 롯 비활성화
    }

    public void PlayerDie(string enemyName)
    {
        // 게임 오버 처리
        if (GameManager.Instance.isGameOver) return;
        GameManager.Instance.isGameOver = true;

        StartCoroutine(DeathDirection(enemyName));
    }

    private IEnumerator DeathDirection(string enemyName)
    {
        gameOverUI.SetActive(true); // 게임 오버 UI 활성화

        // 적 종류에 따른 점프스케어 설정
        GameObject targetScare = null;

        // swtich문으로 적 이름에 따른 점프스케어 오브젝트 할당
        switch (enemyName)
        {
            case "Mut":
                targetScare = mut;
                break;
            case "Lot":
                targetScare = lot;
                break;
            default:
                break;
        }

        if(targetScare != null)
        {
            //점프 스케어 실행
            targetScare.SetActive(true);
        }
        else
        {
            Debug.Log("targetScare is empty!");
            yield break;
        }

        // 점프 스케어 지속 시간 대기
        yield return new WaitForSeconds(scareDuration);

        // 점프 스케어 비활성화
        targetScare.SetActive(false);

        // 커서 잠금 해제
        UnlockCursor();
    }

    // 커서 잠금 해제 처리
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    } 
}
