using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [Header("UI COMPONENTS")]
    [SerializeField] private int numberBall;
    [SerializeField] private TextMeshProUGUI txtBall;
    [SerializeField] private Button btBall;
    [SerializeField] private bool hasRaffled = false;
    [SerializeField] private bool hasSelected = false;
    [SerializeField] private bool hasCanRevoked = false;
    [Header("COMPONENTS")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color confirmedColor;
    [SerializeField] private Color revokebleColor;

    public void InitializeVariables()
    {
        btBall = GetComponent<Button>();
        btBall.onClick.AddListener(SetActionButton);
    }
    public void SetNumberInText(string _numberBall)
    {
        txtBall.text = _numberBall;
        numberBall = System.Convert.ToInt32(_numberBall);
    }
    public string GetNumberBall()
    {
        string numberTxt = numberBall.ToString();
        return numberTxt;
    }
    public void SetActionButton()
    {
        //SetSelectedColor();
        GlobeController globeController = FindObjectOfType<GlobeController>();
        globeController.OpenPanelBall(numberBall);
    }
    public void CheckState()
    {
        if (hasRaffled)
        {
            if (hasCanRevoked)
            {
                btBall.interactable = true;
                SetCanRevokebleColor();
                SetHasSelected(false);
            }
            else
            {
                btBall.interactable = false;
                SetConfirmedColor();
            }
        }
        else
        {
            if (hasSelected)
            {
                SetSelectedColor();
                btBall.interactable = false;
            }
            else
            {
                btBall.interactable = true;
                SetNormalColor();

            }
        }
    }
    public void DisableInteractable()
    {
        btBall.interactable = false;
        txtBall.color = new Color(txtBall.color.r, txtBall.color.g, txtBall.color.b, 0.5f);
    }
    public void EnableInteractable()
    {
        btBall.interactable = true;
        txtBall.color = new Color(txtBall.color.r, txtBall.color.g, txtBall.color.b, 1f);

    }
    public void SetNormalColor()
    {
        btBall.image.color = normalColor;
        txtBall.color = new Color(txtBall.color.r, txtBall.color.g, txtBall.color.b, 1f);
        btBall.interactable = true;
    }
    public void SetSelectedColor()
    {
        btBall.image.color = selectedColor;
    }
    public void SetConfirmedColor()
    {
        btBall.image.color = confirmedColor;
        btBall.interactable = false;
        txtBall.color = new Color(txtBall.color.r, txtBall.color.g, txtBall.color.b, 1f);
    }
    public void SetCanRevokebleColor()
    {
        btBall.image.color = revokebleColor;
        btBall.interactable = true;
        txtBall.color = new Color(txtBall.color.r, txtBall.color.g, txtBall.color.b, 1f);
    }
    public void SetHasRaffled(bool state)
    {
        hasRaffled = state;
    }
    public bool GetHasRaffled()
    {
        return hasRaffled;
    }

    public void SetHasCanRevoked(bool state)
    {
        hasCanRevoked = state;
    }
    public void SetHasSelected(bool state)
    {
        hasSelected = state;
    }
    public bool GetHasCanRevoked()
    {
        return hasCanRevoked;
    }
}
