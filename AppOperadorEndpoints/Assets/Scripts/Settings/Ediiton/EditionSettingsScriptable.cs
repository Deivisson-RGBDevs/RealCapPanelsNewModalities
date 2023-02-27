using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EditionSettings", menuName = "Settings/Edition")]
public class EditionSettingsScriptable : ScriptableObject
{
    public List<edition> allEditions;
    public edition currentEdition;
    [Space]
    [Space]
    public List<edition> allEditionsTest;

    public void PopulateTestInfos()
    {
        allEditions.Clear();
        allEditions.AddRange(allEditionsTest);
    }

    [System.Serializable]
    public class edition
    {
        public string nome;
        public int iD;
        public string numero;
        public string dataRealizacao;
        public string nomePlano;
        public string processoSUSEP;
        public string denominacaoComercial;
        public float tipoTamanhoSerie;
        public int modalidades;
        public string globoTipo;
        public string tipoQuantidadeChances;
        public float valor;
    }
}
