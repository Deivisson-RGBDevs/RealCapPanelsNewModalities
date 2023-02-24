using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TechinicalInfos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtName;
    [SerializeField] TextMeshProUGUI txtCPF;

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
}
