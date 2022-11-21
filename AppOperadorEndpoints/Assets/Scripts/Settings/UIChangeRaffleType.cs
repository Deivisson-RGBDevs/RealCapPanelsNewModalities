using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RiptideNetworking;
using System.Net;
using System.Net.Sockets;

public class UIChangeRaffleType : MonoBehaviour
{
    [SerializeField] private Button btRecovery;
    [SerializeField] private UiInfosRaffle infosRaffle;
    [Header("RAFFLES PANELS")]
    [SerializeField] private GameObject panelRaffleLottery;
    [SerializeField] private GameObject panelRaffleGlobe;
    [SerializeField] private GameObject panelRaffleSpin;

    [Header("BUTTONS RAFFLES")]
    [SerializeField] private Button btRaffleLottery;
    [SerializeField] private Button btRaffleGlobe;
    [SerializeField] private Button btRaffleSpin;

    [Header("BUTTON COLORS")]
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;

    [Header("HIDE RAFFLE SYSTEM")]
    [SerializeField] private Button btVisibilityRaffle;
    [SerializeField] private TextMeshProUGUI txtHideRaffle;
    [SerializeField] private bool canChangeScene = false;

    [SerializeField] private bool hasActiveLottery = true;
    [SerializeField] private bool hasActiveGlobe = true;
    [SerializeField] private bool hasActiveSpin = true;


    void Start()
    {
        InitializeVariables();
    }
    private void InitializeVariables()
    {
        panelRaffleLottery = GameObject.Find("PanelRaffleLottery");
        panelRaffleGlobe = GameObject.Find("PanelRaffleGlobe");
        panelRaffleSpin = GameObject.Find("PanelRaffleSpin");

        if (GameManager.instance.isbackup)
        {
            btRecovery.interactable = true;
        }
        else
        {
            btRecovery.interactable = false;
        }

        txtHideRaffle = btVisibilityRaffle.GetComponentInChildren<TextMeshProUGUI>();

        SetButtonsEvent();
        if (!GameManager.instance.isbackup)
        {
            DefineModalyties(GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].modalidades);
            SendMessageToClientGetActiveScene();
            SetStateButtonsChangeRaffleType();
        }
    }

    private void SetButtonsEvent()
    {
        btRaffleLottery.onClick.AddListener(SetRaffleLottery);
        btRaffleLottery.onClick.AddListener(WriteInfos);
        btRaffleGlobe.onClick.AddListener(SetRaffleGlobe);
        btRaffleGlobe.onClick.AddListener(WriteInfos);
        btRaffleSpin.onClick.AddListener(SetRaffleSpin);
        btRaffleSpin.onClick.AddListener(WriteInfos);
        btVisibilityRaffle.onClick.AddListener(SetStateVisibilityOfRaffle);
        btVisibilityRaffle.onClick.AddListener(WriteInfos);

        ResetColorButtons(normalColor);
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
    #region RAFFLES PANELS

    private void ResetColorButtons(Color newColor)
    {
        btRaffleLottery.image.color = newColor;
        btRaffleGlobe.image.color = newColor;
        btRaffleSpin.image.color = newColor;
    }
    public void SetRaffleLottery()
    {
        SelectPanelForActivate(1);
        infosRaffle.PopulateRaffleInfos(GameManager.instance.lotteryScriptable.sorteioOrdem.ToString(), GameManager.instance.lotteryScriptable.sorteioDescricao, GameManager.instance.lotteryScriptable.sorteioValor);
        LotteryController lotteryController = FindObjectOfType<LotteryController>();
        lotteryController.ResetNumberRaffle();
        SendMessageToClientChangeRaffle("SceneLottery");
    }

    public void SetRaffleGlobe()
    {
        SelectPanelForActivate(2);
        infosRaffle.PopulateRaffleInfos(GameManager.instance.globeScriptable.sorteioOrdem.ToString(), GameManager.instance.globeScriptable.sorteioDescricao, GameManager.instance.globeScriptable.sorteioValor);
        SendMessageToClientChangeRaffle("SceneGlobe");

    }
    public void SetRaffleSpin()
    {
        SelectPanelForActivate(3);
        infosRaffle.PopulateRaffleInfos(GameManager.instance.spinScriptable.sorteioOrdem.ToString(), GameManager.instance.spinScriptable.sorteioDescricao, GameManager.instance.spinScriptable.sorteioValor);

        SendMessageToClientChangeRaffle("SceneSpin");
    }

    public void SelectPanelForActivate(int index)
    {
        switch (index)
        {
            case 0:
                {
                    DefineModalyties(GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].modalidades);
                    break;
                }
            case 1:
                {
                    panelRaffleLottery.SetActive(true);
                    panelRaffleGlobe.SetActive(false);
                    panelRaffleSpin.SetActive(false);
                    ResetColorButtons(normalColor);
                    btRaffleLottery.image.color = selectedColor;
                    break;
                }
            case 2:
                {
                    panelRaffleLottery.SetActive(false);
                    panelRaffleGlobe.SetActive(true);
                    panelRaffleSpin.SetActive(false);
                    ResetColorButtons(normalColor);
                    btRaffleGlobe.image.color = selectedColor;
                    break;
                }
            case 3:
                {
                    panelRaffleLottery.SetActive(false);
                    panelRaffleGlobe.SetActive(false);
                    panelRaffleSpin.SetActive(true);
                    ResetColorButtons(normalColor);
                    btRaffleSpin.image.color = selectedColor;
                    break;
                }
            default:
                {
                    ResetColorButtons(normalColor);
                    panelRaffleLottery.SetActive(false);
                    panelRaffleGlobe.SetActive(false);
                    panelRaffleSpin.SetActive(false);
                    break;
                }
        }
        //SetStateCanChangeScene(true);
    }
    public void DefineModalyties(int type)
    {
        switch (type)
        {
            case 1:
                {
                    SetRaffleLottery();
                    break;
                }
            case 2:
                {
                    SetRaffleLottery();
                    break;
                }
            case 3:
                {
                    SetRaffleGlobe();
                    break;
                }

        }
        SetStateCanChangeScene(true);

    }
    public void DisableAllButtons()
    {
        hasActiveLottery = false;
        hasActiveGlobe = false;
        hasActiveSpin = false;
        // SetStateButtonsChangeRaffleType();
    }
    public void SetStateCanChangeScene(bool _state)
    {
        canChangeScene = _state;
        btVisibilityRaffle.interactable = canChangeScene;

    }
    public void SetStateButtonVisibilityRaffle(bool isActive)
    {
        GetStateVisibilityRaffle(isActive);
    }
    public void SetStateButtonsChangeRaffleType()
    {

        if (hasActiveLottery)
        {
            btRaffleLottery.interactable = canChangeScene;
        }
        else
        {
            btRaffleLottery.interactable = false;
        }
        if (hasActiveGlobe)
        {
            btRaffleGlobe.interactable = canChangeScene;
        }
        else
        {
            btRaffleGlobe.interactable = false;
        }
        if (hasActiveSpin)
        {
            btRaffleSpin.interactable = canChangeScene;
        }
        else
        {
            btRaffleSpin.interactable = false;
        }

    }
    #endregion

    #region HIDE RAFFLE

    public void SetRecoveryConfig()
    {
        RestNetworkManager.instance.CallReadMemory();
    }
    public void GetStateVisibilityRaffle(bool isActive)
    {
        if (isActive)
        {
            txtHideRaffle.text = "OCULTAR SORTEIO";
            btVisibilityRaffle.image.color = selectedColor;
            GameManager.instance.hasVisibleRaffle = false;
        }
        else
        {
            txtHideRaffle.text = "MOSTRAR SORTEIO";
            btVisibilityRaffle.image.color = normalColor;
            GameManager.instance.hasVisibleRaffle = true;
        }
        //SetStateButtonsChangeRaffleType();
    }

    public void SetStateVisibilityOfRaffle()
    {
        if (GameManager.instance.hasVisibleRaffle)
        {
            txtHideRaffle.text = "OCULTAR SORTEIO";
            btVisibilityRaffle.image.color = selectedColor;
            GameManager.instance.hasVisibleRaffle = false;
        }
        else
        {
            txtHideRaffle.text = "MOSTRAR SORTEIO";
            btVisibilityRaffle.image.color = normalColor;
            GameManager.instance.hasVisibleRaffle = true;
        }
        SendMessageVisibilityRaffle();
    }

    public void SendMessageVisibilityRaffle()
    {
        if (!GameManager.instance.hasVisibleRaffle)
        {
            if (panelRaffleLottery.activeSelf == true)
            {
                SendMessageLotteryInfos(
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].numero,
           GameManager.instance.lotteryScriptable.resultadoLoteriaFederalNumeroConcurso,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].dataRealizacao,
           GameManager.instance.lotteryScriptable.resultadoLoteriaFederalDataConcurso,
           GameManager.instance.lotteryScriptable.sorteioOrdem,
           GameManager.instance.lotteryScriptable.sorteioDescricao,
           GameManager.instance.lotteryScriptable.sorteioValor);
            }
            else if (panelRaffleGlobe.activeSelf == true)
            {
                SendMessageGlobeInfos(
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].nome,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].numero,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].dataRealizacao,
           GameManager.instance.globeScriptable.sorteioOrdem,
           GameManager.instance.globeScriptable.sorteioDescricao,
           GameManager.instance.globeScriptable.sorteioValor);
            }
            else if (panelRaffleSpin.activeSelf == true)
            {
                SendMessageSpinInfos(
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].nome,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].numero,
           GameManager.instance.globalScriptable.edicaoInfos[GameManager.instance.EditionIndex].dataRealizacao,
           GameManager.instance.spinScriptable.sorteioOrdem,
           GameManager.instance.spinScriptable.sorteioDescricao,
           GameManager.instance.spinScriptable.sorteioValor);
            }
        }
        TcpNetworkManager.instance.Server.SendToAll(GetMessageBool(Message.Create(MessageSendMode.unreliable, ServerToClientId.messageVisibilityRaffle), GameManager.instance.hasVisibleRaffle));
    }
    private Message GetMessageBool(Message message, bool isActive)
    {
        message.AddBool(isActive);

        return message;
    }

    #endregion

    #region Messages

    public void SendMessageToClientGetActiveScene()
    {
        if (!GameManager.instance.isbackup)
        {
            TcpNetworkManager.instance.Server.SendToAll(GetMessage(Message.Create(MessageSendMode.reliable, ServerToClientId.messageCheckSceneActive)));

        }
    }
    public void SendMessageToClientChangeRaffle(string _messageString)
    {
        if (!GameManager.instance.isbackup)
        {
            SendMessageVisibilityRaffle();
            TcpNetworkManager.instance.Server.SendToAll(GetMessageString(Message.Create(MessageSendMode.reliable, ServerToClientId.messageTypeRaffle), _messageString));
        }
    }
    public void SendMessageLotteryInfos(string _editionNumber, string __competitionNumber, string _dateRaffle, string _dateCompetition, int _ordem, string _description, string _value)
    {
        if (!GameManager.instance.isbackup)
        {
            TcpNetworkManager.instance.Server.SendToAll(GetMessageLotteryInfos(Message.Create(MessageSendMode.reliable, ServerToClientId.messageInfosLottery), _editionNumber, __competitionNumber, _dateRaffle, _dateCompetition, _ordem, _description, _value));
        }
    }
    public void SendMessageGlobeInfos(string _editionName, string _editionNumber, string _date, int _ordem, string _description, string _value)
    {
        if (!GameManager.instance.isbackup)
            TcpNetworkManager.instance.Server.SendToAll(GetMessageGlobeInfos(Message.Create(MessageSendMode.reliable, ServerToClientId.messageInfosGlobe), _editionName, _editionNumber, _date, _ordem, _description, _value));
    }
    public void SendMessageSpinInfos(string _editionName, string _editionNumber, string _date, int _ordem, string _description, string _value)
    {
        if (!GameManager.instance.isbackup)
            TcpNetworkManager.instance.Server.SendToAll(GetMessageSpinInfos(Message.Create(MessageSendMode.reliable, ServerToClientId.messageInfosSpin), _editionName, _editionNumber, _date, _ordem, _description, _value));
    }
    private Message GetMessageString(Message message, string _textMessage)
    {
        message.AddString(_textMessage);

        return message;
    }
    private Message GetMessage(Message message)
    {
        return message;
    }

    private Message GetMessageLotteryInfos(Message message, string _editionNumber, string __competitionNumber, string _dateRaffle, string _dateCompetition, int _ordem, string _description, string _value)
    {
        message.AddString(_editionNumber);
        message.AddString(__competitionNumber);
        message.AddString(_dateRaffle);
        message.AddString(_dateCompetition);
        message.AddInt(_ordem);
        message.AddString(_description);
        message.AddString(_value);

        return message;
    }
    private Message GetMessageGlobeInfos(Message message, string _editionName, string _editionNumber, string _date, int _ordem, string _description, string _value)
    {
        message.AddString(_editionName);
        message.AddString(_editionNumber);
        message.AddString(_date);
        message.AddInt(_ordem);
        message.AddString(_description);
        message.AddString(_value);

        return message;
    }

    private Message GetMessageSpinInfos(Message message, string _editionName, string _editionNumber, string _date, int _ordem, string _description, string _value)
    {
        message.AddString(_editionName);
        message.AddString(_editionNumber);
        message.AddString(_date);
        message.AddInt(_ordem);
        message.AddString(_description);
        message.AddString(_value);

        return message;
    }
    #endregion
}