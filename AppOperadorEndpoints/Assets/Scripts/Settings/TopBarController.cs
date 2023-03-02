using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopBarController : MonoBehaviour
{
    [SerializeField] private FadeController fade;

    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCPF;
    [SerializeField] private Button btSelectEdition;
    [SerializeField] private Button btSelectDraw;
    [SerializeField] private Button btEditionData;
    [SerializeField] private Button btVisibilityDraw;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckStateButtons;
    } private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckStateButtons;
    }
    void Start()
    {
        SetInfo(GameManager.instance.userSettings.tecnicoNome, GameManager.instance.userSettings.tecnicoCPF);
        btSelectEdition.onClick.AddListener(GoToSceneSelectEdition);
        btSelectDraw.onClick.AddListener(GoToSceneSelectDraw);
    }
    public void SetInfo(string _name, string _cpf)
    {
        txtName.text = $"Tecnico: {_name}";
        txtCPF.text = $"CPF: {_cpf}";
    }

    public void ClearFieldInfo()
    {
        txtName.text = string.Empty;
        txtCPF.text = string.Empty;
    }

    void CheckStateButtons(Scene scene, LoadSceneMode mode)
    {   
        if(!GameManager.instance.isBackup)
        {
            btSelectEdition.interactable = false;
            btVisibilityDraw.interactable = false;
            btEditionData.interactable = false;
            btSelectDraw.interactable = false;
            switch(scene.name)
            {
                case "SelectEdition":
                    {
                        btSelectEdition.interactable = false;
                        btVisibilityDraw.interactable = false;
                        btEditionData.interactable = false;
                        btSelectDraw.interactable = false;
                        break;
                    }
                case "SelectDraw":
                    {
                        btEditionData.interactable = true;
                        btSelectEdition.interactable = true;
                        break;
                    }
                case "Globe":
                    {
                        btSelectEdition.interactable = true;
                        btVisibilityDraw.interactable = true;
                        btEditionData.interactable = true;
                        btSelectDraw.interactable = true;
                        break;
                    }
                case "Spin":
                    {
                        btSelectEdition.interactable = true;
                        btVisibilityDraw.interactable = true;
                        btEditionData.interactable = true;
                        btSelectDraw.interactable = true;
                        break;
                    }
                case "Lottery":
                    {
                        btSelectEdition.interactable = true;
                        btVisibilityDraw.interactable = true;
                        btEditionData.interactable = true;
                        btSelectDraw.interactable = true;
                        break;
                    }
            }
        }
    }

    private void GoToSceneSelectEdition()
    {
        fade = FindObjectOfType<FadeController>();
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("SelectEdition");
    }
    private void GoToSceneSelectDraw()
    {
        fade = FindObjectOfType<FadeController>();
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("SelectDraw");
    }
}
