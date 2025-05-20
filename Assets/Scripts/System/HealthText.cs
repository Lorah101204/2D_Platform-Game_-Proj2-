using TMPro;
using UnityEngine;
using DG.Tweening;

public class HealthText : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0, 50, 0);
    public float fadeTime = 0.5f;

    private RectTransform textTransform;
    private TextMeshProUGUI text;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        textTransform.DOMove(textTransform.position + moveOffset, fadeTime).SetEase(Ease.OutQuad);

        text.DOFade(0f, fadeTime).SetEase(Ease.InQuad);

        Destroy(gameObject, fadeTime);
    }
}
