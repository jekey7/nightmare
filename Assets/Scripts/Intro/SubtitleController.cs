using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitleText; // 자막

    // 시그널에서 자막 내용 입력 후 적용
    public void SetSubtitle(string text)
    {
        subtitleText.text = text; // 자막 적용
    }
}
