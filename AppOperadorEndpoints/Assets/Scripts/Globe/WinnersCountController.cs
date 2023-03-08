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
       
    }
    private void OnEnable()
    {
        GlobeManager.OnUpdateScreen += UpdateDrawnBalls;
    }
    private void OnDisable()
    {
        GlobeManager.OnUpdateScreen -= UpdateDrawnBalls;
    }

    private void UpdateDrawnBalls()
    {
        if (GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count > 5)
        {
            SetAmountWinners(2);
            SetAmountOneBall(7);
            SetAmountTwoBalls(50);

            listTicketsController.testeInfos = new List<string> { "Bola 03 - 3° Chance - N° 432130", "Bola 45 - 2° Chance - N° 272180" };
            listTicketsController.PopulateListTickets(listTicketsController.testeInfos, true);
        }
        
        else if (GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count >= 3 && GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count<=20)
            {
                SetAmountWinners(0);
                SetAmountOneBall(3);
                SetAmountTwoBalls(35);

                listTicketsController.PopulateListTickets(listTicketsController.testeInfos, false);
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
