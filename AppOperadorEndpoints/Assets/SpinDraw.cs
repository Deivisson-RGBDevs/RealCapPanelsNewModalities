using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SpinDraw : MonoBehaviour
{
    public static event Action<TicketInfos> OnShowticket;

    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtSpinNumber;
    [SerializeField] private Button btDrawNumber;
    [SerializeField] private Button btShowTicket;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private int index;
    public bool hasDrawn = false;

    private void Start()
    {
        btDrawNumber.onClick.AddListener(SetSpinNumber);
        btShowTicket.onClick.AddListener(ShowTicket);
        btShowTicket.interactable = false;
    }
    public void SetActive(bool _isActive)
    {
        canvasGroup.interactable = _isActive;
        if(_isActive)
        {
            canvasGroup.alpha = 1f;
            txtSpinNumber.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0.5f;
            txtSpinNumber.alpha = 0.5f;
        }
    }
    public void SetIndex(int _index)
    {
        index = _index;
    }
    public void PopulateTitle(int _index)
    {
        txtTitle.text = $"Sortear {_index}° Giro";
    }
    
    public void SetSpinNumber()
    {
        txtSpinNumber.text = GameManager.instance.spinResultScriptable.ganhadorContemplado[index].numeroSorte;
        btShowTicket.interactable = true;
    }

    public void ShowTicket()
    {
        hasDrawn = true;
        OnShowticket?.Invoke(GameManager.instance.spinResultScriptable.ganhadorContemplado[index]);
    }

}
