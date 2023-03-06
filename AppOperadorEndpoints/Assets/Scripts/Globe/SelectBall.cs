using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SelectBall : MonoBehaviour
{
    public static event Action<bool, int> OnSelectedNumber;

    [SerializeField] private TextMeshProUGUI txtNumberBall;
    [SerializeField] private int numberBall;
    [SerializeField] private bool isDrawn;

    [SerializeField] private Button btNumber;
    void Start()
    {
        btNumber = GetComponent<Button>();
        btNumber.onClick.AddListener(SelectNumber);
    }

    private void SelectNumber()
    {
        OnSelectedNumber?.Invoke(isDrawn, numberBall);
    }
    public void SetNumberInText(int _number)
    {
        numberBall = _number;
        txtNumberBall.text = _number.ToString("00");
    }
}
