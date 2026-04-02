using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField] private float hp = 500f; // 체력 값

    public event Action<Health> OnDied; // 사망 이벤트

    public float InitialHp { get; private set; } // 초기 체력 값
    public float Hp // 현재 체력 값
    {
        get { return hp; }
        set { hp = value; }
    }

    private void Start()
    {
        InitialHp = hp; // 초기 체력 값 저장
    }

    // 데미지 처리
    public void TakeDamaged(float damage)
    {
        hp -= damage; // 체력 감소

        // 체력이 0 이하라면 사망했다고 스포너에 알림
        if (hp <= 0f)
            OnDied?.Invoke(this);
    }
}
