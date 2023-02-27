using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserSettings", menuName = "Settings/UserSettings")]
public class UserSettingsScriptable : ScriptableObject
{
    public string tecnicoNome;
    public string tecnicoCPF;

    public void SetInfosSettings(string _tecnicoName, string _tecnicoCPF)
    {
        tecnicoNome = _tecnicoName;
        tecnicoCPF = _tecnicoCPF;
    }


}
