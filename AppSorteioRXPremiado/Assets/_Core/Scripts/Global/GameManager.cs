using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Globalization;
using System;
public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager _instance;

    public static GameManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }
    #endregion


    public static event Action OnNewBallRecieve;
    public GlobeScriptable globeScriptable;
    public LuckySpinScriptable luckySpinScriptable;


    // [SerializeField] private FadeController fade;
    [SerializeField] public bool isConnected = false;
    [SerializeField] public bool isLotteryOpenedScreen = false;
    [SerializeField] private string currentSceneName;
    [SerializeField] private int sceneIndex;

    public Camera cameraActive;
    private void Start()
    {
        InitializeVariables();
        CallChangeSceneRaffle("Globe");
    }
    public string FormatMoneyInfo(float value, int decimalHouse = 2)
    {
        string prizeFormated = string.Format(CultureInfo.CurrentCulture, value.ToString($"C{decimalHouse}"));
        return prizeFormated;
    }
    private void InitializeVariables()
    {
        Invoke("ConnectServer", 1f);
        Application.targetFrameRate = 60;
        globeScriptable.ResetRaffle();
        //WinnersScreen.instance.SetWinnersScreenVisibility(false, 0.1f);
        //TicketScreen.instance.SetTicketVisibility(false, 0.1f);
    }
    public void ConnectServer()
    {
        //Codigo de Conexão com o Server
    }
    public void SetCamActiveInCanvas(Camera main)
    {
        WinnersScreen winners = FindObjectOfType<WinnersScreen>();
        winners.GetComponent<Canvas>().worldCamera = main;

        TicketScreen ticket = FindObjectOfType<TicketScreen>();
        ticket.GetComponent<Canvas>().worldCamera = main;
    }
    public void ResetScene()
    {
        globeScriptable.ResetRaffle();
        SceneManager.LoadScene(currentSceneName);
    }
    public void CallChangeSceneRaffle(string sceneName)
    {
        StartCoroutine(ChangeSceneRaffle(sceneName));
    }
    private IEnumerator ChangeSceneRaffle(string sceneName)
    {
        if (currentSceneName != sceneName)
        {
            currentSceneName = sceneName;
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
            yield return new WaitForSeconds(0.5f);
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }
    public int GetSceneIndex()
    {
        return sceneIndex;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Application.Quit();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int number = 0;
            number = UnityEngine.Random.Range(1, 61);
            if (!globeScriptable.ballsDrawn.Contains(number))
            {
                print(number.ToString("00"));
                globeScriptable.AddNewBall(number);
                OnNewBallRecieve?.Invoke();
            }
        }
    }
}


