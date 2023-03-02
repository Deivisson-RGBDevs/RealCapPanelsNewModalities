using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinnersCountController : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI txtWinnersCount;
    [SerializeField] private TextMeshProUGUI txtOneBallCount;
    [SerializeField] private TextMeshProUGUI txtTwoBallsCount;

    [Header("REFERENCES")]
    [SerializeField] private ListTicketsController listTicketsController;


    void Start()
    {
        SetAmountWinners(0);
        SetAmountOneBall(3);
        SetAmountTwoBalls(35);
    }

    public void SetAmountWinners(int _amount)
    {
        txtWinnersCount.text = _amount.ToString();
    }
    public void SetAmountOneBall(int _amount)
    {
        txtOneBallCount.text = _amount.ToString();
    }
    public void SetAmountTwoBalls(int _amount)
    {
        txtTwoBallsCount.text = _amount.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
