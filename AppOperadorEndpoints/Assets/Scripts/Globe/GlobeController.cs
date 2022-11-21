using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RiptideNetworking;
using System.Collections;

public class GlobeController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private TicketController ticketController;
    [SerializeField] public Button btTicketVisibility;
    [SerializeField] public Button btPrizeVisibility;
    [SerializeField] public Button btNextRaffle;
    [Header("GRID BALLS")]
    [SerializeField] private List<Ball> balls;
    [SerializeField] private GameObject panelGridBalls;
    [SerializeField] private int indexBallSelected;
    [SerializeField] private int indexOrder;

    [Header("GRID RAFFLED BALLS")]
    [SerializeField] private Ball[] ballsRaffled;
    [SerializeField] private Ball ballRaffledPrefab;
    [SerializeField] private GameObject panelGridBallsRaffled;
    [SerializeField] private Ball lastBallRaffled;
    [SerializeField] private List<int> indexBalls;


    [Header("TEXT INFOS")]
    [SerializeField] private TextMeshProUGUI txtInfosTitle;
    [SerializeField] private TextMeshProUGUI txtForOneBall;
    [SerializeField] private TextMeshProUGUI txtWinners;
    [SerializeField] private TextMeshProUGUI txtForTwoBalls;

    [Header("WINNERS")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private PossibleWinners[] possiblesWinners;
    [SerializeField] private PossibleWinners possibleWinnerGO;
    [SerializeField] private int indexWinner = 0;

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

    [SerializeField] private bool isWinner = false;
    [SerializeField] private bool hasRevoked = false;

   
    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        balls = new List<Ball>();
        balls.AddRange(panelGridBalls.GetComponentsInChildren<Ball>());
        ticketController = FindObjectOfType<TicketController>();
        PopulateBalls();
        btNextRaffle.interactable = false;
        //UpdateScreen();
        UpdateStateVisibilityButtonsTicket(false);

        if(GameManager.instance.isbackup)
        {
            SetDisableAll();
            UIChangeRaffleType uIChangeRaffle = FindObjectOfType<UIChangeRaffleType>();
            uIChangeRaffle.DisableAllButtons();
        }
       
    }

    public void UpdateStateVisibilityButtonsTicket(bool isActive)
    {
        btTicketVisibility.interactable = isActive;
        btPrizeVisibility.interactable = isActive;
    }
    #region BALLS 
    private void PopulateBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            int number = i + 1;
            balls[i].InitializeVariables();
            balls[i].SetNumberInText(number.ToString());
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

        if (GameManager.instance.globeRaffleScriptable.ganhadorContemplado.Length > 0)
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
    private void SetDisableAll()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].DisableInteractable();
        }
    }
    private void SetEnableAll()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].EnableInteractable();
        }
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
            if (!isWinner)
            {
                panelConfirmBall.SetActive(true);
            }
        }
        SetTxtViewBall(balls[indexBallSelected].GetNumberBall());
        CheckStateBalls();
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
        for (int i = 0; i < GameManager.instance.globeRaffleScriptable.bolasSorteadas.Count; i++)
        {
            indexBalls.Add(int.Parse(GameManager.instance.globeRaffleScriptable.bolasSorteadas[i]) - 1);
            
            //balls[int.Parse(GameManager.instance.globeRaffleScriptable.bolasSorteadas[i]) - 1].SetHasSelected(false);
            balls[int.Parse(GameManager.instance.globeRaffleScriptable.bolasSorteadas[i]) - 1].SetHasRaffled(true);
        }
        CheckStateBalls();
        ValidateBall();
    }
    public void GetInfosFromServer()
    {
        RestNetworkManager.instance.SendBallsRaffledFromServer(GameManager.instance.globeScriptable.sorteioOrdem, true);
    }
    public void SendBallsRaffledToScreen()
    {
        UpdateScreen();
        if (hasRevoked)
        {
            SendMessageBallRevoked();
        }
        else
        {
            SendMessageBallsRaffled();
        }

    }
    #region BALLS RAFFLED

    private void SpawnBallRaffled(List<string> _balls)
    {
        foreach (var item in ballsRaffled)
        {
            Destroy(item.gameObject);
        }
        ballsRaffled = new Ball[_balls.Count];
        for (int i = 0; i < _balls.Count; i++)
        {
            Ball inst = Instantiate(ballRaffledPrefab, transform.position, Quaternion.identity);
            inst.transform.SetParent(panelGridBallsRaffled.transform);
            inst.SetNumberInText(_balls[i]);
            inst.transform.localScale = new Vector3(1, 1, 1);
            if (i == _balls.Count - 1)
            {
                inst.SetCanRevokebleColor();
            }
            else
            {
                inst.SetConfirmedColor();
            }
            ballsRaffled[i] = inst;
        }
        orderBalls.text = $"Dezenas Sorteadas: {ballsRaffled.Length}";
    }
    #endregion
    public void PopulatePossibleWinners()
    {
        List<string> infos = new List<string>();

        if (possiblesWinners.Length > 0)
        {
            for (int i = 0; i < possiblesWinners.Length; i++)
            {
                Destroy(possiblesWinners[i].gameObject);
            }
        }

        if (GameManager.instance.globeRaffleScriptable.ganhadorContemplado.Length > 0)
        {
            txtInfosTitle.text = "GANHADOR";
            GameManager.instance.globeRaffleScriptable.porUmaBolas.Clear();
            infos = GameManager.instance.GetWinners();
            possiblesWinners = new PossibleWinners[infos.Count];
            if (GameManager.instance.globeRaffleScriptable.ganhadorContemplado.Length > 1)
                txtInfosTitle.text = "GANHADORES";
            txtWinners.text = infos.Count.ToString();
            isWinner = true;
            UpdateStateVisibilityButtonsTicket(true);

            btNextRaffle.interactable = true;
            SetDisableAllNotRevoke();
        }
        else
        {
            UpdateStateVisibilityButtonsTicket(false);

            possiblesWinners = new PossibleWinners[0];
            infos = GameManager.instance.GetForOneBalls();
            possiblesWinners = new PossibleWinners[infos.Count];
            txtForOneBall.text = infos.Count.ToString();
            txtWinners.text = "0";
            if (infos.Count > 0)
            {
                txtInfosTitle.text = "POR UMA BOLA";
            }
            else
            {
                txtInfosTitle.text = "INFORMA��ES";
            }
            isWinner = false;
        }
        txtForTwoBalls.text = GameManager.instance.GetForTwoBalls();

        for (int i = 0; i < infos.Count; i++)
        {
            PossibleWinners inst = Instantiate(possibleWinnerGO);
            inst.PopulateInfos(infos[i]);
            inst.SetIndex(i);
            inst.transform.SetParent(contentParent.transform);
            inst.transform.localScale = new Vector2(1, 1);
            possiblesWinners[i] = inst;
        }
        SetInteractablePossiblesWinners(isWinner);
    }
    public void SetInteractablePossiblesWinners(bool _isActive)
    {
        for (int i = 0; i < possiblesWinners.Length; i++)
        {
            possiblesWinners[i].SetInteractableButton(_isActive);
        }
    }
    public void ResetPossiblesWinners()
    {
        foreach (var item in possiblesWinners)
        {
            item.DesactiveIsSelect();
        }
    }
    private void WriteInfos()
    {
        GameManager.instance.configScriptable.UpdateConfig(
                GameManager.instance.sceneId,
                GameManager.instance.globeRaffleScriptable.bolasSorteadas,
                GameManager.instance.globeScriptable.sorteioOrdem,
                GameManager.instance.hasVisibleRaffle
                );
    }
    public void ConfirmBallSelected()
    {
        GameManager.instance.SetNewBall(balls[indexBallSelected].GetNumberBall());
        hasRevoked = false;
        //SetDisableAll();
        WriteInfos();
    }
    public void RevokeBallSelected()
    {
        indexBalls.Remove(indexBalls[indexBalls.Count - 1]);
        GameManager.instance.SetRemoveBall(balls[indexBallSelected].GetNumberBall());
        lastBallRaffled.SetHasSelected(true);
        lastBallRaffled.SetHasRaffled(false);
        lastBallRaffled.SetHasCanRevoked(false);
       // SetDisableAll();

        hasRevoked = true;
        WriteInfos();

    }
    public void ValidateBall()
    {
        PopulatePossibleWinners();
        if (indexBalls.Count > 0)
        {
            lastBallRaffled = balls[indexBalls[indexBalls.Count - 1]];
            lastBallRaffled.SetHasCanRevoked(true);
        }

        CheckStateBalls();

        SpawnBallRaffled(GameManager.instance.GetBallsRaffled());
    }
    public void CallNextRaffle()
    {
        StartCoroutine(NextRaffle());
    }
    private IEnumerator NextRaffle()
    {
        UIChangeRaffleType uIChangeRaffle = FindObjectOfType<UIChangeRaffleType>();
        UiInfosRaffle uiInfos = FindObjectOfType<UiInfosRaffle>();
        uIChangeRaffle.SetStateVisibilityOfRaffle();
        GameManager.instance.globeScriptable.sorteioOrdem++;
        GameManager.instance.ResetScreenGlobe();
        yield return new WaitForSeconds(1);
        SendMesageNextRaffle();
        DisableHasRevokedAll();
        if (possiblesWinners.Length > 0)
        {
            for (int i = 0; i < possiblesWinners.Length; i++)
            {
                Destroy(possiblesWinners[i].gameObject);
            }
        }
        possiblesWinners = new PossibleWinners[0];

        yield return new WaitForSeconds(0.3f);
        UpdateScreen();
        uIChangeRaffle.SendMessageGlobeInfos(
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].nome,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].numero,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].dataRealizacao,
           GameManager.instance.globeScriptable.sorteioOrdem,
           GameManager.instance.globeScriptable.sorteioDescricao,
           GameManager.instance.globeScriptable.sorteioValor);
       
        uiInfos.PopulateRaffleInfos(
            GameManager.instance.globeScriptable.sorteioOrdem.ToString(), 
            GameManager.instance.globeScriptable.sorteioDescricao, 
            GameManager.instance.globeScriptable.sorteioValor);

        btNextRaffle.interactable = false;
    }

    public void SetInteractableBtNextRaffle(bool isActive)
    {
        btNextRaffle.interactable = isActive;
    }

    private void FixedUpdate()
    {
        if(GameManager.instance.hasVisibleRaffle)
        {
            SetDisableAll();
        }
        else
        {
            SetEnableAll();
        }
    }
    public void ShowTicketGlobe()
    {
        foreach (var item in possiblesWinners)
        {
            if (item.GetIsSelected() == true)
            {
                indexWinner = item.GetIndex();
            }
        }

        ticketController.PopulateTicketInfos(
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].nome,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].cpf,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].dataNascimento,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].telefone,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].email,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].endereco,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].numero,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].complemento,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].bairro,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].municipio,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].cep,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].estado,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].dataSorteio,
            GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].numero,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].valor,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].PDV,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].bairoPDV,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].dataCompra,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].horaCompra,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].numeroTitulo,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].chance,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].numeroCartela,
            GameManager.instance.globeRaffleScriptable.ganhadorContemplado[indexWinner].numeroSorte,
            true,
            2);
    }

   
    #region MESSAGES
    public void SendMessageBallsRaffled()
    {
        if (!GameManager.instance.isbackup)
            TcpNetworkManager.instance.Server.SendToAll(GetMessageString(Message.Create(MessageSendMode.reliable, ServerToClientId.messageBall), GameManager.instance.GetBallsRaffled().ToArray(), GameManager.instance.globeRaffleScriptable.porUmaBolas.Count, GameManager.instance.globeRaffleScriptable.ganhadorContemplado.Length, GameManager.instance.globeRaffleScriptable.valorPremio.ToString()));
    }
    public void SendMessageBallRevoked()
    {
        if (!GameManager.instance.isbackup)
            TcpNetworkManager.instance.Server.SendToAll(GetMessageBallRevoked(Message.Create(MessageSendMode.reliable, ServerToClientId.messageBallRevoked), GameManager.instance.GetBallsRaffled().ToArray(), GameManager.instance.globeRaffleScriptable.porUmaBolas.Count, GameManager.instance.globeRaffleScriptable.ganhadorContemplado.Length, GameManager.instance.globeRaffleScriptable.valorPremio.ToString()));
    }

    public void SendMesageNextRaffle()
    {
        if (!GameManager.instance.isbackup)
            TcpNetworkManager.instance.Server.SendToAll(GetMessage(Message.Create(MessageSendMode.reliable, ServerToClientId.messageNextRaffleGlobe)));
    }

    private Message GetMessageString(Message message, string[] _ballsRaffled, int _forOneBall, int _winnersCount, string _prizeValue)
    {
        message.AddStrings(_ballsRaffled);
        message.AddInt(_forOneBall);
        message.AddInt(_winnersCount);
        message.AddString(_prizeValue);
        return message;
    }

    private Message GetMessageBallRevoked(Message message, string[] _ballsRaffled, int _forOneBall, int _winnersCount, string _prizeValue)
    {
        message.AddStrings(_ballsRaffled);
        message.AddInt(_forOneBall);
        message.AddInt(_winnersCount);
        message.AddString(_prizeValue);

        return message;
    }

    private Message GetMessage(Message message)
    {
        return message;
    }

    #endregion






}
