using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EditionInfoCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtInfo;
    [SerializeField] private string info;
    void Start()
    {
        SetInfo(" ");
    }

    public void SetInfo(string _info)
    {
        txtInfo.text = _info;
    }

    public void ClearFieldInfo()
    {
        txtInfo.text = string.Empty;
        info = string.Empty;
    }
}
