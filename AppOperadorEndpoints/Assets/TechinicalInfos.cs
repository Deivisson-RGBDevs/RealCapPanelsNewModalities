using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TechinicalInfos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCPF;
    [SerializeField] private Button btSelectEdition;
    [SerializeField] private Button btSelectDraw;
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
            if(scene.name!="Globe"|| scene.name != "Spin"|| scene.name != "Lottery")
            {
                btSelectEdition.interactable = false;
                btVisibilityDraw.interactable = false;
                btSelectDraw.interactable = false;
            }
            else
            {
                btSelectEdition.interactable = true;
                btVisibilityDraw.interactable = true;
                btSelectDraw.interactable = true;
            }
        }
    }
}
