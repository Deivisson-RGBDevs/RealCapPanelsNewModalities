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
        public string name;
        public int id;
        public int number;
        public string date;
        public string namePlan;
        public string processSUSEP;
        public string commercialName;
        public int sizeSeries;
        public int modality;
        public string typeGlobe;
        public string chances;
        public float value;
        [Space]
        public List<drawinfos> lotteryInfos;
        public List<drawinfos> globeInfos;
        public List<drawinfos> spinInfos;
    }

    [System.Serializable]
    public class drawinfos
    {
        public string description;
        public int order;
        public float value;
    }
}
