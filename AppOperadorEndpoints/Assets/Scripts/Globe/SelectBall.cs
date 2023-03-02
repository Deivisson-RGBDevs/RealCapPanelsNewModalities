using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectBall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtNumberBall;
    [SerializeField] private int numberBall;

    void Start()
    {
        
    }

    public void SetNumberInText(int _number)
    {
        numberBall = _number;
        txtNumberBall.text = _number.ToString("00");
    }
}
