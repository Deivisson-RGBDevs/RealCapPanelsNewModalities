using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SelectBall : MonoBehaviour
{
    public static event Action<bool, int> OnSelectedNumber;

    [SerializeField] private Button btNumber;
    [SerializeField] private TextMeshProUGUI txtNumberBall;
    [SerializeField] private int numberBall;
    [SerializeField] private bool hasDrawn = false;
    [SerializeField] private bool canRevocable = false;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color drawnColor;
    [SerializeField] private Color revocableColor;

    [SerializeField] private Color startColorText;
    void Start()
    {
        btNumber = GetComponent<Button>();
        btNumber.onClick.AddListener(SelectNumber);
        startColorText = txtNumberBall.color;
    }
    public void SetHasDrawn(bool _hasDrawn)
    {
        hasDrawn = _hasDrawn;
        SetInteractableButton(false);
        if (_hasDrawn)
        {
            SetNewColor(drawnColor);
            txtNumberBall.color = normalColor;
            btNumber.GetComponentInChildren<TextMeshProUGUI>().alpha = 0.5f;
        }
        else
        {
            txtNumberBall.color = startColorText;
        }
    }
    public void SetNumberInText(int _number)
    {
        numberBall = _number;
        txtNumberBall.text = _number.ToString("00");
    }
    public void SetCanRevoked(bool _canRevoked)
    {
        canRevocable = _canRevoked;
        SetInteractableButton(true);
        if (_canRevoked)
        {
            SetNewColor(revocableColor);
            txtNumberBall.color = normalColor;

        }
        else
        {
            SetNewColor(normalColor);
            txtNumberBall.color = startColorText;
            btNumber.GetComponentInChildren<TextMeshProUGUI>().alpha = 1;
        }

    }
    public bool GetCanRevocable()
    {
        return canRevocable;
    }
    public int GetNumberBall()
    {
        return numberBall;
    }
    private void SelectNumber()
    {
        OnSelectedNumber?.Invoke(hasDrawn, numberBall);
    }

    public void SetInteractableButton(bool _isActive)
    {
        btNumber.interactable = _isActive;
    }

    private void SetNewColor(Color _newColor)
    {
        btNumber.image.color = _newColor;
    }
}
