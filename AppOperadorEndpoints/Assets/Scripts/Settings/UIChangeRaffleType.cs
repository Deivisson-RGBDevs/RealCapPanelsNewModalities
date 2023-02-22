using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIChangeRaffleType : MonoBehaviour
{
    [Header("CONTROLLERS")]
    [SerializeField] GlobeController globeController;

    [Header("GERAL")]
    [SerializeField] private Button btRecovery;
    [SerializeField] private UiInfosRaffle infosRaffle;
    [Header("RAFFLES PANELS")]
    [SerializeField] private GameObject panelRaffleLottery;
    public GameObject panelRaffleGlobe;
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

    [SerializeField] private bool hasActiveLottery = true;
    [SerializeField] private bool hasActiveGlobe = true;
    [SerializeField] private bool hasActiveSpin = true;

    void Start()
    {
        InitializeVariables();
    }
    private void InitializeVariables()
    {
        SetModality();
        SetButtonsEvent();
        GameManager.instance.isVisibleRaffle = GameManager.instance.technicalScriptable.isVisibleRaffle;
        GameManager.instance.RecoveryGlobeScreen();
        SetStateSelectBackupButton();
        CheckStateVisibilityRaffle();
        RestNetworkManager.instance.CallWriteMemory();
    }

    private void SetModality()
    {
        switch (GameManager.instance.editionScriptable.edicaoInfos[GameManager.instance.EditionIndex].modalidades)
        {
            case 1:
                {
                    hasActiveLottery = true;
                    hasActiveGlobe = false;
                    hasActiveSpin = false;
                    SetRaffleLottery();
                    break;
                }
            case 2:
                {
                    hasActiveLottery = true;
                    hasActiveGlobe = true;
                    hasActiveSpin = true;
                    SetRaffleGlobe();
                    break;
                }
            case 3:
                {
                    hasActiveLottery = false;
                    hasActiveGlobe = true;
                    hasActiveSpin = true;
                    SetRaffleGlobe();
                    break;
                }
        }
        btRaffleLottery.interactable = hasActiveLottery;
        btRaffleGlobe.interactable = hasActiveGlobe;
        btRaffleSpin.interactable = hasActiveSpin;
    }
    private void SetButtonsEvent()
    {
        btRaffleLottery.onClick.AddListener(SetRaffleLottery);
        btRaffleLottery.onClick.AddListener(GameManager.instance.WriteInfosGlobe);
        btRaffleGlobe.onClick.AddListener(SetRaffleGlobe);
        btRaffleGlobe.onClick.AddListener(GameManager.instance.WriteInfosGlobe);
        btRaffleSpin.onClick.AddListener(SetRaffleSpin);
        btRaffleSpin.onClick.AddListener(GameManager.instance.WriteInfosGlobe);
        btVisibilityRaffle.onClick.AddListener(SetStateHasRaffleVisibility);
        btVisibilityRaffle.onClick.AddListener(GameManager.instance.WriteInfosGlobe);
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
        LotteryController lotteryController = FindObjectOfType<LotteryController>();
        lotteryController.ResetNumberRaffle();
    }

    public void SetRaffleGlobe()
    {
        SelectPanelForActivate(2);

    }
    public void SetRaffleSpin()
    {
        SelectPanelForActivate(3);
    }

    public void SelectPanelForActivate(int index)
    {
        switch (index)
        {
            case 1:
                {
                    panelRaffleLottery.SetActive(true);
                    ResetColorButtons(normalColor);
                    panelRaffleGlobe.SetActive(false);
                    panelRaffleSpin.SetActive(false);
                    btRaffleLottery.image.color = selectedColor;
                    infosRaffle.PopulateRaffleInfos(GameManager.instance.lotteryScriptable.sorteioOrdem.ToString(), GameManager.instance.lotteryScriptable.sorteioDescricao, GameManager.instance.lotteryScriptable.sorteioValor);

                    break;
                }
            case 2:
                {
                    ResetColorButtons(normalColor);
                    panelRaffleLottery.SetActive(false);
                    panelRaffleGlobe.SetActive(true);
                    panelRaffleSpin.SetActive(false);
                    btRaffleGlobe.image.color = selectedColor;
                    infosRaffle.PopulateRaffleInfos(GameManager.instance.globeScriptable.GetGlobeOrder().ToString(), GameManager.instance.globeScriptable.GetGlobeDescription(), GameManager.instance.globeScriptable.GetGlobeValue());
                    break;
                }
            case 3:
                {
                    ResetColorButtons(normalColor);
                    panelRaffleLottery.SetActive(false);
                    panelRaffleGlobe.SetActive(false);
                    panelRaffleSpin.SetActive(true);
                    btRaffleSpin.image.color = selectedColor;
                    infosRaffle.PopulateRaffleInfos(GameManager.instance.spinScriptable.sorteioOrdem.ToString(), GameManager.instance.spinScriptable.sorteioDescricao, GameManager.instance.spinScriptable.sorteioValor);
                    SpinController spinController = FindObjectOfType<SpinController>();
                    spinController.ActiveButtonNewRaffleSpin();
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
        GameManager.instance.sceneId = index;
        GameManager.instance.technicalScriptable.panelActive = index;
    }
    public void RaffleTypeScene(int type)
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
                    SetRaffleGlobe();
                    break;
                }
            case 3:
                {
                    SetRaffleSpin();
                    break;
                }
        }
    }

    #endregion

    #region RECOVERY

    public void SetRecoveryConfig()
    {
        if (GameManager.instance.isBackup)
        {
            RestNetworkManager.instance.DisableInvokInfosServer();
            GameManager.instance.isBackup = false;
        }
        else
        {
            RestNetworkManager.instance.CallGetInfoServer();
            GameManager.instance.isBackup = true;
        }
        SetStateSelectBackupButton();
    }
    private void SetStateSelectBackupButton()
    {
        if (GameManager.instance.isBackup)
        {
            btRecovery.GetComponentInChildren<TextMeshProUGUI>().text = "Backup";
            btRecovery.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            btRecovery.image.color = Color.red;
        }
        else
        {
            btRecovery.GetComponentInChildren<TextMeshProUGUI>().text = "Principal";
            btRecovery.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            btRecovery.image.color = Color.green;
            globeController.UpdateScreen();

            SpinController spinController = FindObjectOfType<SpinController>();
            if (spinController != null)
            {
                spinController.ActiveLastSpin();
                spinController.ActiveButtonNewRaffleSpin();
            }
        }
    }
    #endregion

    #region VISIBILITY RAFFLE

    private void SetStateButtonsRaffle(bool isActive)
    {
        if (GameManager.instance.isBackup)
        {
            btRaffleLottery.interactable = false;
            btRaffleGlobe.interactable = false;
            btRaffleSpin.interactable = false;
        }
        else
        {
            if (hasActiveLottery)
            {
                btRaffleLottery.interactable = isActive;
            }
            else
            {
                btRaffleLottery.interactable = false;
            }

            if (hasActiveGlobe)
            {
                btRaffleGlobe.interactable = isActive;
            }
            else
            {
                btRaffleGlobe.interactable = false;
            }

            if (hasActiveSpin)
            {
                btRaffleSpin.interactable = isActive;
            }
            else
            {
                btRaffleSpin.interactable = false;
            }
        }
    }
    public void SetStateHasRaffleVisibility()
    {
        if (GameManager.instance.isVisibleRaffle)
        {
            GameManager.instance.isVisibleRaffle = false;
        }
        else
        {
            GameManager.instance.isVisibleRaffle = true;
        }
        CheckStateVisibilityRaffle();
    }
    public void CheckStateVisibilityRaffle()
    {
        if (GameManager.instance.isVisibleRaffle)
        {
            btVisibilityRaffle.GetComponentInChildren<TextMeshProUGUI>().text = "OCULTAR SORTEIO";
            btVisibilityRaffle.image.color = selectedColor;
            SetStateButtonsRaffle(false);
            globeController.SetEnableAll();
        }
        else
        {
            btVisibilityRaffle.GetComponentInChildren<TextMeshProUGUI>().text = "MOSTRAR SORTEIO";
            btVisibilityRaffle.image.color = normalColor;
            SetStateButtonsRaffle(true);
            globeController.SetDisableAll();
        }
    }
    #endregion

}
