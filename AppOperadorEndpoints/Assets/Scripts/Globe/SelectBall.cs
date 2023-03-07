using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SelectBall : MonoBehaviour
{
    public static event Action<bool, int> OnSelectedNumber;

    [SerializeField] private TextMeshProUGUI txtNumberBall;
    [SerializeField] private int numberBall;
    [SerializeField] private bool hasDrawn = false;
    [SerializeField] private bool canRevocable = false;

    [SerializeField] private Button btNumber;
    void Start()
    {
        btNumber = GetComponent<Button>();
        btNumber.onClick.AddListener(SelectNumber);
    }
    public void SetHasDrawn(bool _hasDrawn)
    {
        hasDrawn = _hasDrawn;
        SetInteractableButton(false);
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
    }
    public int GetNumberBall()
    {
        return numberBall;
    }
    private void SelectNumber()
    {
        OnSelectedNumber?.Invoke(hasDrawn, numberBall);
    }

    private void SetInteractableButton(bool _isActive)
    {
        btNumber.interactable = _isActive;
    }
}
