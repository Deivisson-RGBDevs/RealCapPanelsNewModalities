using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinnersCountController : MonoBehaviour
{
    public static event Action<bool> OnWinners;
    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI txtWinnersCount;
    [SerializeField] private TextMeshProUGUI txtOneBallCount;
    [SerializeField] private TextMeshProUGUI txtTwoBallsCount;

    [Header("REFERENCES")]
    [SerializeField] private ListTicketsController listTicketsController;


    void Start()
    {

    }
    private void OnEnable()
    {
        GlobeManager.OnUpdateScreen += VerifyWithWinners;
    }
    private void OnDisable()
    {
        GlobeManager.OnUpdateScreen -= VerifyWithWinners;
    }

    private void VerifyWithWinners()
    {
        listTicketsController.ResetGrid();
        if (GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count > 5)
        {
            SetAmountWinners(2);
            SetAmountOneBall(7);
            SetAmountTwoBalls(50);

            listTicketsController.testeInfos = new List<string> { "Bola 03 - 3° Chance - N° 432130", "Bola 45 - 2° Chance - N° 272180" };
            listTicketsController.PopulateListTickets(listTicketsController.testeInfos, true);
            OnWinners?.Invoke(true);
        }

        else if (GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count >= 3 && GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count <= 20)
        {
            SetAmountWinners(0);
            SetAmountOneBall(3);
            SetAmountTwoBalls(35);

            listTicketsController.PopulateListTickets(listTicketsController.testeInfos, false);
            OnWinners?.Invoke(false);
        }

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
}
