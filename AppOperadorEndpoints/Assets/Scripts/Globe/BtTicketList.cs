using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BtTicketList : MonoBehaviour
{
    private TextMeshProUGUI textInfo;
    private Button button;

    [SerializeField] private bool isSelected = false;
    [SerializeField] private bool isFinished = false;
    [SerializeField] private int index;
    [SerializeField] private string infos;

    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color finishedColor;

    void Start()
    {
        InitializeVariables();
    }
    private void OnEnable()
    {
        InitializeVariables();
    }
    void InitializeVariables()
    {
        textInfo = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectWinner);
    }
    public void SelectWinner()
    {
        GlobeOldController globe = FindObjectOfType<GlobeOldController>();
        globe.ResetPossiblesWinners();
        globe.UpdateStateVisibilityButtonsTicket(true);
        GameManager.instance.ticketWinnerIndex = index;
        button.image.color = selectedColor;
        isSelected = true;
        GameManager.instance.WriteInfosGlobe();

    }
    public void SetInteractableButton(bool _isActive)
    {
        button.interactable = _isActive;
    }
    public void SetNormalColor()
    {
        button.image.color = normalColor;
    }
    public void SetIndex(int _index)
    {
        index = _index;
    }
    public int GetIndex()
    {
        return index;
    }
    public bool GetIsSelected()
    {
        return isSelected;
    }
    public void SetIsFinished(bool _isActive)
    {
        isFinished = _isActive;
        isSelected = false;
    }
    public void DesactiveIsSelect()
    {
        if (isFinished)
        {
            button.image.color = finishedColor;
        }
        else
        {
            button.image.color = normalColor;
        }
        isSelected = false;
    }
    public void PopulateInfos(string _infos)
    {
        textInfo = GetComponentInChildren<TextMeshProUGUI>();
        infos = _infos;
        textInfo.text = infos;
    }
}
