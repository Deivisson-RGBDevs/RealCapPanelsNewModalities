using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
public class UiLoginManager : MonoBehaviour
{
    [Header("GERAL REFERENCES")]
    [SerializeField] private FadeController fadeController;

    [Header("COMPONENTS")]
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputIPAddress;
    [SerializeField] private TextMeshProUGUI txtMessage;

    public Button btSelectBackup;
    public Button btConfirm;
    public bool isBackup = false;

  
    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        fadeController = FindObjectOfType<FadeController>();
        btSelectBackup.onClick.AddListener(SelectBackup);
        PopulateFieldIPAddress(NetworkManager.instance.baseUrl1);
    }

    private void OnEnable()
    {
        NetworkManager.OnLogin += CallbackLoginStatus;
    }
    private void OnDisable()
    {
        NetworkManager.OnLogin -= CallbackLoginStatus;
    }
    private void PopulateFieldIPAddress(string url)
    {
        inputIPAddress.text = url;
    }
    private void SetNewIPAddress()
    {
        NetworkManager.instance.baseUrl1 = inputIPAddress.text;
    }
    public void SelectBackup()
    {
        if (GameManager.instance.isBackup)
        {
            GameManager.instance.isBackup = false;
            btSelectBackup.image.color = Color.white;
        }
        else
        {
            GameManager.instance.isBackup = true;
            btSelectBackup.image.color = new Color(1, 0.77f, 0, 1);
        }
    }
    public void Login()
    {
        SetNewIPAddress();
        btConfirm.interactable = false;
        txtMessage.text = string.Empty;
        NetworkManager.instance.RequestLogin(inputUsername.text, inputPassword.text);
    }


    private void CallbackLoginStatus(bool _isActive)
    {
        if (_isActive == true)
        {
            txtMessage.text = "Login efetuado com Sucesso!";
            StartCoroutine(GoToSceneSelectEdition());
        }
        else
        {
            btConfirm.interactable = true;
        }
    }

    private IEnumerator GoToSceneSelectEdition()
    {
        yield return new WaitForSeconds(1f);
        fadeController.SetStateFadeOUT();
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadSceneGame("SelectEdition");
    }

    //public void PopulateDropdownEditions()
    //{
    //    dropdown.ClearOptions();
    //    List<string> newOptions = new List<string>();

    //    for (int i = 0; i < GameManager.instance.editionScriptable.edicaoInfos.Count; i++)
    //    {
    //        string option = $"{GameManager.instance.editionScriptable.edicaoInfos[i].numero} - {GameManager.instance.editionScriptable.edicaoInfos[i].nome} - {GameManager.instance.editionScriptable.edicaoInfos[i].dataRealizacao}";
    //        newOptions.Add(option);
    //    }
    //    dropdown.AddOptions(newOptions);
    //}
    //public void ConfirmSelection()
    //{
    //    StartCoroutine(SelectEdition());
    //}
    //private IEnumerator SelectEdition()
    //{
    //    GameManager.instance.SetEditionIndex(dropdown.value);
    //    fadeController.SetStateFadeOUT();
    //    yield return new WaitForSeconds(1f);
    //    GameManager.instance.LoadSceneGame("MainScene");
    //}

}
