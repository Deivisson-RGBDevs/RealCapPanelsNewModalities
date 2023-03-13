using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public static event Action OnPopulateRaffles;

    public RecoveryScriptable recoveryScriptable;
    [Space]
    public UserSettingsScriptable userSettings;
    public EditionSettingsScriptable editionSettings;
    public TechnicalScriptable technicalScriptable;
    [Space]
    public LotteryScriptable lotteryScriptable;
    public LotteryResultScriptable lotteryResultScriptable;
    [Space]
    public GlobeScriptable globeScriptable;
    public GlobeDrawScriptable globeDrawnScriptable;
    [Space]
    public SpinScriptable spinScriptable;
    public SpinResultScriptable spinResultScriptable;

    [Header("Settings")]
    public bool isBackup = false;
    public bool isConnected = false;
    public bool isVisibleRaffle = false;
    public bool isWinner = false;
    public bool isTicketVisible = false;
    public int ticketWinnerIndex = 0;
    public int indexSpinDraw = 0;

    public int EditionIndex { get; private set; }

    public void SetEditionIndex(int value)
    {
        EditionIndex = value;
    }
    public int sceneId;
    void Start()
    {
        globeDrawnScriptable.bolasSorteadas.Clear();

        //technicalScriptable.ResetInfos();
        //recoveryScriptable.ResetInfos();
    }
    private void OnEnable()
    {
        WinnersCountController.OnWinners += SetIsWinner;
    }
    private void OnDisable()
    {
        WinnersCountController.OnWinners -= SetIsWinner;
    }

    private void SetIsWinner(bool _isWinner)
    {
        isWinner = _isWinner;
    }
    public void RecoveryGlobeScreen()
    {
        //GlobeOldController globeController = FindObjectOfType<GlobeOldController>();
        //if (globeController != null)
        //{
        //    if (isBackup)
        //    {
        //        TicketController ticket = FindObjectOfType<TicketController>();
        //        if (isWinner == true)
        //        {
        //            globeController.PopulateTicketGlobe();
        //            ticket.CheckStateVisibility();
        //            globeController.UpdateStateVisibilityButtonsTicket(false);
        //        }
        //        globeController.UpdateScreen();
        //    }
        //}

        //UIChangeRaffleType uIChangeRaffleType = FindObjectOfType<UIChangeRaffleType>();
        //if (uIChangeRaffleType != null)
        //{
        //    uIChangeRaffleType.CheckStateVisibilityRaffle();
        //    uIChangeRaffleType.SelectPanelForActivate(technicalScriptable.panelActive);
        //}
    }
    public void RecoverySpinScreen()
    {
        SpinController spinController = FindObjectOfType<SpinController>();
        TicketController ticket = FindObjectOfType<TicketController>();
        if (spinController != null)
        {
            spinController.PopulateSpinsFields(technicalScriptable.spinNumbers);
            spinController.PopulateTicketSpin();
            ticket.CheckStateVisibility();
            spinController.ShowSpinOrder(technicalScriptable.spinIndex);
        }

        //UIChangeRaffleType uIChangeRaffleType = FindObjectOfType<UIChangeRaffleType>();
        //if (uIChangeRaffleType != null)
        //{
        //    uIChangeRaffleType.CheckStateVisibilityRaffle();
        //    uIChangeRaffleType.SelectPanelForActivate(technicalScriptable.panelActive);
        //}
    }
    public void LoadSceneGame(string map)
    {
        StartCoroutine(ChangeScene(map));
    }
    private IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(0.8f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public string FormatMoneyInfo(float value, int decimalHouse = 2)
    {
        string prizeFormated = string.Format(CultureInfo.CurrentCulture, value.ToString($"C{decimalHouse}"));
        return prizeFormated;
    }

    public decimal ConvertIntToDec(int _x, int _powBy)
    {
        return _x / (decimal)Math.Pow(10.00, _powBy);
    }
    public void CallEventLogin()
    {
        isConnected = true;
        OnPopulateRaffles?.Invoke();
    }

    #region GLOBE FUNCTIONS
    public void SetNewBall(int newBall)
    {
        if (!globeDrawnScriptable.bolasSorteadas.Contains(newBall))
        {
            globeDrawnScriptable.SetNewBall(newBall);
            //NetworkManager.instance.SendBallsRaffledFromServer();
        }
    }
    public List<int> GetBallsRaffled()
    {
        return globeDrawnScriptable.bolasSorteadas;
    }
    public void SetRemoveBall(int newBall)
    {
        if (globeDrawnScriptable.bolasSorteadas.Contains(newBall))
        {
            globeDrawnScriptable.RevokeBall(newBall);
            //NetworkManager.instance.SendBallsRaffledFromServer();
        }
    }
    public void PopulateListOfVisibleTicket()
    {
        globeDrawnScriptable.ticketListVisible = new bool[globeDrawnScriptable.ganhadorContemplado.Length];
    }
    public void SetIsVisibleTicketList(int index)
    {
        globeDrawnScriptable.ticketListVisible[index] = true;
    }
    public bool GetAllTicketsVisible()
    {
        int index = 0;
        for (int i = 0; i < globeDrawnScriptable.ticketListVisible.Length; i++)
        {
            if (globeDrawnScriptable.ticketListVisible[i] == true)
            {
                index++;
            }
        }
        if (index >= globeDrawnScriptable.ticketListVisible.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<string> GetForOneBalls()
    {
        List<string> forOneBalls = new List<string>();

        for (int i = 0; i < globeDrawnScriptable.porUmaBolas.Count; i++)
        {
            forOneBalls.Add($"{globeDrawnScriptable.porUmaBolas[i].numeroBola} - {globeDrawnScriptable.porUmaBolas[i].numeroChance} - {globeDrawnScriptable.porUmaBolas[i].numeroTitulo}");
        }
        return forOneBalls;
    }

    public List<string> GetWinners()
    {
        List<string> winners = new List<string>();

        for (int i = 0; i < globeDrawnScriptable.ganhadorContemplado.Length; i++)
        {
            winners.Add($"{globeDrawnScriptable.ganhadorContemplado[i].numeroTitulo} - {globeDrawnScriptable.ganhadorContemplado[i].chance} ");
        }
        return winners;
    }
    public string GetForTwoBalls()
    {
        string result = string.Empty;
        result = globeDrawnScriptable.porDuasBolas.ToString();
        return result;
    }
    public string GetWinnersCount()
    {
        string result = string.Empty;
        result = globeDrawnScriptable.ganhadorContemplado.ToString();
        return result;
    }

    public void ResetScreenGlobe()
    {
        globeDrawnScriptable.bolasSorteadas.Clear();
        globeDrawnScriptable.porUmaBolas.Clear();
        globeDrawnScriptable.porDuasBolas = 0;
        globeDrawnScriptable.ganhadorContemplado = new TicketInfos[0];

        WriteInfosGlobe();
    }
    #endregion

    public void WriteInfosGlobe()
    {
        technicalScriptable.UpdateConfig(sceneId, globeScriptable.GetGlobeOrder(), isVisibleRaffle, globeDrawnScriptable.porDuasBolas, globeDrawnScriptable.porUmaBolas
            , globeDrawnScriptable.ganhadorContemplado.ToList(),
            globeDrawnScriptable.ticketListVisible.ToList(),
           ticketWinnerIndex, instance.isTicketVisible);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Application.Quit();
            }
        }

        //if (globeDrawnScriptable.ganhadorContemplado.Length > 0)
        //{
        //    isWinner = true;
        //}
        //else
        //{
        //    isWinner = false;
        //}

    }

    [Serializable]
    public class RequestBallsRaffled
    {
        public List<int> balls;
        public int sorteioOrdem;
    }


    [Serializable]
    public class RequestSpin
    {
        public int sorteioOrdem;
    }
}

