using System.Collections;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform firePoint; // 총구 위치
    [SerializeField] private ParticleSystem gunFlashEffect; // 총구 화염 효과
    [SerializeField] private Light gunFlashLight; // 총구 화염 조명
    [SerializeField] private Animator gunAnimator; // 총 애니메이터
    [SerializeField] private WeaponRecoil weaponRecoil; // 반동 구현 스크립트
    [SerializeField] private MagController magController; // 탄창 관리 스크립트
    
    [Header("Gun Settings")]
    [SerializeField] private float gunDamage = 10f; // 총 데미지
    [SerializeField] private float shootRange = 100f; // 유효 사거리
    [SerializeField] private float fireRate = 0.5f; // 연사 속도

    [Header("Sound Settings")]
    [SerializeField] private AudioSource audioSource; // 오디오 소스
    [SerializeField] private AudioClip fireClip;

    [HideInInspector]
    public bool isAiming = false; // 조준 중인가

    private float currentFireRate = 0f; // 현재 연사 속도 카운터
   
    private void Start()
    {
        // 변수 초기화
        currentFireRate = fireRate;
        gunFlashLight.enabled = false;

        // 커서 잠금
        SetCursor();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return; // 게임 종료 시 업데이트 중지

        TimeTick(); // 시간 경과 처리

        // 재장전 중일 때 사격 불가
        if (magController.isReloading) return;

        Aiming(); // 조준 처리

        // 사격 전 탄약 체크
        if (magController.isAmmoEmpty)
        {
            // 탄약 없음 사운드 출력
            return;
        }
        GunFire(); // 사격 처리
    }

    // 커서 잠금 처리
    private void SetCursor()
    {
        // 커서 잠금
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    // 시간 경과 처리
    private void TimeTick()
    {
        // 현재 시간 += 델타 타임
        currentFireRate += Time.deltaTime;
        // 연사 속도 최대치 넘지 않도록 제한
        if (currentFireRate > fireRate)
            currentFireRate = fireRate;
    }

    // 조준 처리
    private void Aiming()
    {
        // 마우스 우클릭 조준 (토글)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isAiming = !isAiming; // 조준 전환
            gunAnimator.SetBool("isAiming", isAiming); // 애니메이션 동작
        }
    }

    // 사격 처리
    private void GunFire()
    {
        // 사격 입력 및 연사 속도 체크
        if (Input.GetKey(KeyCode.Mouse0) && currentFireRate >= fireRate)
        {
            // 사격 방향 설정
            Vector3 fireDir = firePoint.transform.forward; 

            // 사격 효과 처리
            gunAnimator.SetTrigger("Fire"); // 애니메이션 동작
            weaponRecoil.ApplyRecoil(); // 반동 실시
            StartCoroutine(GunFlash()); // 총구 화염 효과
            audioSource.PlayOneShot(fireClip); // 발포음 재생

            // 레이캐스트로 피격 처리
            RaycastHit hit;
            if (Physics.Raycast(firePoint.transform.position, fireDir, out hit, shootRange))
            {
                // Enemy 명중 시 피격 처리
                if (hit.transform.CompareTag("Enemy"))
                {
                    print("Hit");
                    hit.transform.GetComponent<Health>().TakeDamaged(gunDamage); // 데미지 적용
                }
            }

            // 탄약 소모 처리
            magController.UpdateAmmo();

            // 연사 속도 초기화
            currentFireRate = 0f;
        }
    }

    // 총구 화염 처리
    private IEnumerator GunFlash()
    {
        gunFlashEffect.Play(); // 총구 화염 파티클 재생
        gunFlashLight.enabled = true; // 총구 화염 조명 On

        // 총구 화염 조명은 파티클 지속 시간 동안만 유지
        yield return new WaitForSeconds(gunFlashEffect.main.duration);

        gunFlashLight.enabled = false; // 총구 화염 조명 Off
    }
}
