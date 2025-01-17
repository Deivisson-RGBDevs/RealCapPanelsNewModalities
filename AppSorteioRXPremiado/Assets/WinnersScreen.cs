using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Globalization;

public class WinnersScreen : MonoBehaviour
{
    public static WinnersScreen instance { get; private set; }

    PrizeImageController prizeImageController;


    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtCount;
    [SerializeField] private TextMeshProUGUI txtRaffleRound;
    [SerializeField] private GameObject winnersPanel;
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


    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        canvasGroup.alpha = 0;
        winnersPanel.transform.DOScale(new Vector3(0, 0, 0), 0.1f);
        prizeImageController = GetComponentInChildren<PrizeImageController>();
    }
    public void SetWinnersScreenVisibility(bool isActive, float timeAnim=1f)
    {

        if (isActive)
        {
            canvasGroup.DOFade(1, timeAnim).OnComplete(() =>
            {
                winnersPanel.transform.DOScale(1, 1f);
                UiGlobeManager uiGlobeManager = FindObjectOfType<UiGlobeManager>();
                uiGlobeManager.ActiveConfets();
            });
        }
        else
        {
            canvasGroup.DOFade(0, timeAnim).OnComplete(() =>
            {
                winnersPanel.transform.DOScale(0,0.1f);
            });
        }
    }

    public void SetInfosWinnerScreen(int _count, float _prize)
    {
        prizeImageController.SetPrizeImage(GameManager.instance.globeScriptable.order);
        float newValue = Mathf.Floor(_prize * 100) / 100;
        string prizeFormated = string.Format(CultureInfo.CurrentCulture, "{0:C2}", newValue);
        if(_count>1)
        {
            txtTitle.text = $"TEMOS {_count}\nGANHADORES";
            txtCount.text = $"{prizeFormated} para cada!";
        }
        else
        {
            txtTitle.text = $"TEMOS {_count}\nGANHADOR";
            txtCount.text = $"valor l�quido de {prizeFormated}";
        }
        txtRaffleRound.text = $"{GameManager.instance.globeScriptable.order}� Sorteio";

    }
}
