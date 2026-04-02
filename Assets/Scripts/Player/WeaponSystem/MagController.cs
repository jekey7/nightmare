using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MagController : MonoBehaviour
{
    [Header("Mag Settings")]
    [SerializeField] private float maxMag = 8f; // 총 탄창 용량

    [Header("Reload Settings")]
    [SerializeField] private KeyCode reloadKey = KeyCode.R; // 재장전 키
    [SerializeField] private float trTime = 2.5f; // 전술 재장전(Tactical Reload) 시간
    [SerializeField] private float erTime = 3.5f; // 완전 재장전(Empty Reload) 시간

    [Header("References")]
    [SerializeField] private Animator gunAnimator; // 총 애니메이터

    public bool isAmmoEmpty = false; // 탄약이 남아 있는가
    public bool isReloading = false; // 재장전 중인가

    private float currentMag; // 현재 탄창에 남은 탄약

    private void Start()
    {
        currentMag = maxMag; // 현재 탄창을 최대치로 초기화
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        // 재장전 키 입력 감지
        if (Input.GetKeyDown(reloadKey) && currentMag != maxMag)
        {
            StartCoroutine(Reload()); // 재장전 실시
            gunAnimator.SetTrigger("Reload");
        }
            

        // 애니메이션 동작
        gunAnimator.SetBool("isAmmoEmpty", isAmmoEmpty);
    }

    public void UpdateAmmo()
    {
        // 탄약 감소
        currentMag--;
        // 탄약 없음 처리

        if (currentMag <= 0)
        {
            // 현재 탄약 = 0(안전 장치)
            currentMag = 0;
            // 탄약 없음 = true
            isAmmoEmpty = true;
            // 즉시 애니메이션 반영
            gunAnimator.SetBool("isAmmoEmpty", isAmmoEmpty);
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        float reloadTime = isAmmoEmpty ? erTime : trTime;
        yield return new WaitForSeconds(reloadTime);
        currentMag = maxMag;
        isAmmoEmpty = false;
        isReloading = false;
    }
}
