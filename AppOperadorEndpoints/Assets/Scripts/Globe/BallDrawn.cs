using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallDrawn : MonoBehaviour
{
    [Header("UI COMPONENTS")]
    [SerializeField] private Image bgBall;
    [SerializeField] private int numberBall;
    [SerializeField] private TextMeshProUGUI txtBall;
    [Space]
    [SerializeField] private bool hasDrawn = false;
    [SerializeField] private bool canRevocable = false;
    [Space]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color drawnColor;
    [SerializeField] private Color revocableColor;
    [SerializeField] private Color startColorText;

    private void Start()
    {
        bgBall = GetComponent<Image>();
        startColorText = txtBall.color;
    }
    public void SetNumberInText(int _numberBall)
    {
        txtBall.text = _numberBall.ToString("D2");
        numberBall = _numberBall;
    }
    public void ClearCell()
    {
        txtBall.text = string.Empty;
        numberBall = 0;
        SetNormalColor();
    }
    public void SetRevocableColor()
    {
        bgBall.color = revocableColor;
        txtBall.color = normalColor;
    }
    private void SetNormalColor()
    {
        bgBall.color = normalColor;
        txtBall.color = startColorText;
    }
    public void SetDrawnColor()
    {
        bgBall.color = drawnColor;
        txtBall.color = normalColor;
    }
}
