using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RiptideNetworking;
using System.Collections;
using System.Linq;

public class GlobeOldController : MonoBehaviour
{
    [SerializeField] public Button btNextRaffle;
    [Header("TICKET INFOS")]
    [SerializeField] private TicketController ticketController;
    [SerializeField] public Button btTicketVisibility;
    [Header("GRID BALLS")]
    [SerializeField] private List<BallDrawn> balls;
    [SerializeField] private GameObject panelGridBalls;
    [SerializeField] private int indexBallSelected;

    [Header("GRID RAFFLED BALLS")]
    [SerializeField] private BallDrawn[] ballsRaffled;
    [SerializeField] private BallDrawn ballRaffledPrefab;
    [SerializeField] private GameObject panelGridBallsRaffled;
    [SerializeField] private BallDrawn lastBallRaffled;
    [SerializeField] private List<int> indexBalls;


    [Header("TEXT INFOS")]
    [SerializeField] private TextMeshProUGUI txtInfosTitle;
    [SerializeField] private TextMeshProUGUI txtForOneBall;
    [SerializeField] private TextMeshProUGUI txtWinners;
    [SerializeField] private TextMeshProUGUI txtForTwoBalls;

    [Header("WINNERS")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private BtTicketList[] possiblesWinners;
    [SerializeField] private BtTicketList possibleWinnerGO;

    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI orderBalls;
    [Header("CONFIRM BALL")]
    [SerializeField] private Button btConfirm;
    [SerializeField] private GameObject panelConfirmBall;
    [SerializeField] private TextMeshProUGUI txtViewNumberBallConfirm;
    [Header("REVOKE BALL")]
    [SerializeField] private Button btRevoke;
    [SerializeField] private GameObject panelRevokeBall;
    [SerializeField] private TextMeshProUGUI txtViewNumberBallRevoke;

    [SerializeField] private bool hasRevoked = false;


    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        balls = new List<BallDrawn>();
        balls.AddRange(panelGridBalls.GetComponentsInChildren<BallDrawn>());
        ticketController = FindObjectOfType<TicketController>();
        PopulateBalls();
        btNextRaffle.interactable = false;
        UpdateScreen();
        UpdateStateVisibilityButtonsTicket(false);

        if (GameManager.instance.isBackup)
        {
            SetDisableAll();
        }
        GameManager.instance.RecoveryGlobeScreen();
    }
    public void UpdateStateVisibilityButtonsTicket(bool isActive)
    {
        if (GameManager.instance.isBackup)
        {
            btTicketVisibility.interactable = false;
        }
        else
        {
            btTicketVisibility.interactable = isActive;
        }
    }
    #region BALLS 
    private void PopulateBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            int number = i + 1;
            balls[i].InitializeVariables();
            balls[i].SetNumberInText(number);
        }
    }
    public void SetTxtViewBall(string _number)
    {
        if (panelConfirmBall.activeSelf)
        {
            txtViewNumberBallConfirm.text = _number;
        }
        if (panelRevokeBall.activeSelf)
        {
            txtViewNumberBallRevoke.text = _number;
        }
    }
    private void CheckStateBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].CheckState();
        }

        if (GameManager.instance.globeDrawnScriptable.ganhadorContemplado.Length > 0)
        {
            SetDisableAllNotRevoke();
        }
    }
    public void DisableHasRevokedAll()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].SetHasCanRevoked(false);
            balls[i].SetHasSelected(false);
            balls[i].SetHasRaffled(false);
        }
    }
    public void DisableAllConfirmed()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].SetHasRaffled(false);
        }
    }
    private void SetDisableAllNotRevoke()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (!balls[i].GetHasCanRevoked())
            {
                balls[i].DisableInteractable();
            }
        }
    }
    public void SetDisableAll()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].DisableInteractable();
        }
    }
    public void SetEnableAll()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].EnableInteractable();
        }
        CheckStateBalls();
    }
    public void OpenPanelBall(int index)
    {
        indexBallSelected = index - 1;

        if (balls[indexBallSelected].GetHasRaffled() == true)
        {
            panelRevokeBall.SetActive(true);
        }
        else
        {
            balls[indexBallSelected].SetHasSelected(true);
            if (!GameManager.instance.isWinner)
            {
                panelConfirmBall.SetActive(true);
            }
        }
        //SetTxtViewBall(balls[indexBallSelected].GetNumberBall());
        CheckStateBalls();
    }
    public void ConfirmBallSelected()
    {
       // GameManager.instance.SetNewBall(balls[indexBallSelected].GetNumberBall());
        hasRevoked = false;

    }
    public void RevokeBallSelected()
    {
        indexBalls.Remove(indexBalls[indexBalls.Count - 1]);
        //GameManager.instance.SetRemoveBall(balls[indexBallSelected].GetNumberBall());
        lastBallRaffled.SetHasSelected(true);
        lastBallRaffled.SetHasRaffled(false);
        lastBallRaffled.SetHasCanRevoked(false);

        hasRevoked = true;

    }
    public void ClosePanelConfirmBall()
    {
        panelConfirmBall.SetActive(false);
        balls[indexBallSelected].SetHasSelected(false);
        CheckStateBalls();
    }
    public void ClosePanelRevokeBall()
    {
        panelRevokeBall.SetActive(false);
        balls[indexBallSelected].SetHasSelected(false);
        CheckStateBalls();
    }
    #endregion

    public void UpdateScreen()
    {
        DisableHasRevokedAll();
        indexBalls.Clear();
        for (int i = 0; i < GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count; i++)
        {
            indexBalls.Add(GameManager.instance.globeDrawnScriptable.bolasSorteadas[i] - 1);

            balls[GameManager.instance.globeDrawnScriptable.bolasSorteadas[i] - 1].SetHasRaffled(true);
        }
        CheckStateBalls();
        ValidateBall();
        CheckBtNextRaffle();
        if (GameManager.instance.technicalScriptable.ticketsShown.Count > 0)
            CheckWinnerButtonState(GameManager.instance.technicalScriptable.ticketsShown, GameManager.instance.technicalScriptable.currentTicketIndex);
    }

    public void SendBallsRaffledToScreen()
    {
        UpdateScreen();
    }
    #region BALLS RAFFLED

    private void SpawnBallRaffled(List<string> _balls)
    {
        //foreach (var item in ballsRaffled)
        //{
        //    Destroy(item.gameObject);
        //}
        //ballsRaffled = new BallDrawn[_balls.Count];
        //for (int i = 0; i < _balls.Count; i++)
        //{
        //    BallDrawn inst = Instantiate(ballRaffledPrefab, transform.position, Quaternion.identity);
        //    inst.transform.SetParent(panelGridBallsRaffled.transform);
        //    inst.SetNumberInText(_balls[i]);
        //    inst.transform.localScale = new Vector3(1, 1, 1);
        //    if (i == _balls.Count - 1)
        //    {
        //        inst.SetCanRevokebleColor();
        //    }
        //    else
        //    {
        //        inst.SetConfirmedColor();
        //    }
        //    ballsRaffled[i] = inst;
        //}
        //orderBalls.text = $"Dezenas Sorteadas: {ballsRaffled.Length}";
    }
    #endregion

    private void ResetPanelPossibleWinners()
    {
        if (possiblesWinners.Length > 0)
        {
            for (int i = 0; i < possiblesWinners.Length; i++)
            {
                Destroy(possiblesWinners[i].gameObject);
            }
        }
    }
    public void PopulateWinners(List<string> _infos)
    {
        txtInfosTitle.text = "GANHADOR";
        GameManager.instance.globeDrawnScriptable.porUmaBolas.Clear();
        _infos = GameManager.instance.GetWinners();
        possiblesWinners = new BtTicketList[_infos.Count];
        if (GameManager.instance.globeDrawnScriptable.ganhadorContemplado.Length > 1)
            txtInfosTitle.text = "GANHADORES";
        txtWinners.text = _infos.Count.ToString();
        GameManager.instance.isWinner = true;
        txtForOneBall.text = "0";
        SetDisableAllNotRevoke();

        for (int i = 0; i < _infos.Count; i++)
        {
            BtTicketList inst = Instantiate(possibleWinnerGO);
            inst.PopulateInfos(_infos[i]);
            inst.SetIndex(i);

            inst.transform.SetParent(contentParent.transform);
            inst.transform.localScale = new Vector2(1, 1);
            possiblesWinners[i] = inst;
        }
        possiblesWinners[0].SelectWinner();
    }
    private void PopulatePossibleWinners(List<string> _infos)
    {
        UpdateStateVisibilityButtonsTicket(false);

        possiblesWinners = new BtTicketList[0];
        _infos = GameManager.instance.GetForOneBalls();
        possiblesWinners = new BtTicketList[_infos.Count];
        txtForOneBall.text = _infos.Count.ToString();
        txtWinners.text = "0";
        if (_infos.Count > 0)
        {
            txtInfosTitle.text = "POR UMA BOLA";
        }
        else
        {
            txtInfosTitle.text = "INFORMA��ES";
        }

        for (int i = 0; i < _infos.Count; i++)
        {
            BtTicketList inst = Instantiate(possibleWinnerGO);
            inst.PopulateInfos(_infos[i]);
            inst.SetIndex(i);

            inst.transform.SetParent(contentParent.transform);
            inst.transform.localScale = new Vector2(1, 1);
            possiblesWinners[i] = inst;
        }
        GameManager.instance.isWinner = false;
    }
    public void CheckWinners()
    {
        List<string> infos = new List<string>();

        ResetPanelPossibleWinners();
        if (GameManager.instance.globeDrawnScriptable.ganhadorContemplado.Length > 0)
        {
            PopulateWinners(infos);
        }
        else
        {
            PopulatePossibleWinners(infos);
        }
        txtForTwoBalls.text = GameManager.instance.GetForTwoBalls();

        SetInteractablePossiblesWinners(GameManager.instance.isWinner);
        GameManager.instance.WriteInfosGlobe();
    }
    public void SetInteractablePossiblesWinners(bool _isActive)
    {
        for (int i = 0; i < possiblesWinners.Length; i++)
        {
            if (GameManager.instance.isBackup)
            {
                possiblesWinners[i].SetInteractableButton(false);
            }
            else
            {
                possiblesWinners[i].SetInteractableButton(_isActive);
                possiblesWinners[i].SetNormalColor();
            }
        }
    }
    public void ResetPossiblesWinners()
    {
        foreach (var item in possiblesWinners)
        {
            item.DesactiveIsSelect();
        }
    }

    public void ValidateBall()
    {
        CheckWinners();
        if (indexBalls.Count > 0)
        {
            lastBallRaffled = balls[indexBalls[indexBalls.Count - 1]];
            lastBallRaffled.SetHasCanRevoked(true);
        }

        CheckStateBalls();

        //SpawnBallRaffled(GameManager.instance.GetBallsRaffled());
    }
    public void CallNextRaffle()
    {
        StartCoroutine(NextRaffle());
    }
    private IEnumerator NextRaffle()
    {
        UIChangeRaffleType uIChangeRaffle = FindObjectOfType<UIChangeRaffleType>();
        UiInfosRaffle uiInfos = FindObjectOfType<UiInfosRaffle>();
        GameManager.instance.globeScriptable.SetGlobeOrder(GameManager.instance.globeScriptable.GetGlobeOrder() + 1);
        GameManager.instance.ResetScreenGlobe();
        yield return new WaitForSeconds(1);
        DisableHasRevokedAll();
        if (possiblesWinners.Length > 0)
        {
            for (int i = 0; i < possiblesWinners.Length; i++)
            {
                Destroy(possiblesWinners[i].gameObject);
            }
        }
        possiblesWinners = new BtTicketList[0];
        NetworkManager.instance.CallInfosGlobe();
        yield return new WaitForSeconds(0.2f);
        UpdateScreen();
        uIChangeRaffle.SendMessageGlobeInfos(
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].name,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].number.ToString(),
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].date,
            GameManager.instance.globeScriptable.GetGlobeOrder(),
            GameManager.instance.globeScriptable.GetGlobeDescription(),
            GameManager.instance.globeScriptable.GetGlobeValue());

        CheckBtNextRaffle();
        NetworkManager.instance.SendBallsRaffledFromServer();
    }

    public void CheckWinnerButtonState(List<bool> _ticketsShown, int index)
    {
        for (int i = 0; i < _ticketsShown.Count; i++)
        {
            if (_ticketsShown[i] == true)
            {
                possiblesWinners[i].SetIsFinished(true);
            }
            possiblesWinners[i].DesactiveIsSelect();
        }
        possiblesWinners[index].SelectWinner();
    }
    public void ShowTicketGlobe()
    {
        foreach (var item in possiblesWinners)
        {
            if (item.GetIsSelected() == true)
            {
                GameManager.instance.ticketWinnerIndex = item.GetIndex();
                GameManager.instance.SetIsVisibleTicketList(item.GetIndex());

                item.SetIsFinished(true);
            }
        }
        CheckBtNextRaffle();
        PopulateTicketGlobe();
        ticketController.SetTicketVisibility();
    }

    private void CheckBtNextRaffle()
    {
        if (GameManager.instance.globeScriptable.GetGlobeOrder() < GameManager.instance.recoveryScriptable.limit_globo && GameManager.instance.isWinner == true)
            btNextRaffle.interactable = GameManager.instance.GetAllTicketsVisible();
        else
            btNextRaffle.interactable = false;
    }
    public void PopulateTicketGlobe()
    {
        ticketController.PopulateTicketInfos(
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].nome,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].cpf,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].dataNascimento,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].telefone,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].email,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].bairro,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].municipio,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].estado,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].dataSorteio,
           GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].number,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].valor,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].PDV,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].bairoPDV,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].dataCompra,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].horaCompra,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].numeroTitulo,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].chance,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].numeroCartela,
           GameManager.instance.globeDrawnScriptable.ganhadorContemplado[GameManager.instance.ticketWinnerIndex].numeroSorte,
           true,
           2);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (panelConfirmBall.activeSelf)
            {
                ConfirmBallSelected();
                panelConfirmBall.SetActive(false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (panelConfirmBall.activeSelf)
            {
                ClosePanelConfirmBall();
            }
        }
    }
}

