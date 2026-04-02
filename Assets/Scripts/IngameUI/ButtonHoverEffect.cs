using UnityEngine;
using UnityEngine.EventSystems; // UI 이벤트 필수
using DG.Tweening; // 닷트윈

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float duration = 0.2f;
    private Vector3 originalScale;

    private void Start()
    {
        // 본래 크기 저장
        originalScale = transform.localScale;
    }

    // 마우스가 들어왔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 기존 애니메이션 멈추고 새로운 크기로 변경
        transform.DOScale(originalScale * hoverScale, duration).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    // 마우스가 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        // 원래 크기로 복구
        transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
}
