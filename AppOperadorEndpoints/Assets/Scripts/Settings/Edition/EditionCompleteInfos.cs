using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionCompleteInfos : MonoBehaviour
{
    [SerializeField] private List<EditionInfoCard> infosCards;


    private void OnEnable()
    {
        PopulateEditionInfos(
            GameManager.instance.editionSettings.currentEdition.name,
            GameManager.instance.editionSettings.currentEdition.number,
            GameManager.instance.editionSettings.currentEdition.date,
            GameManager.instance.editionSettings.currentEdition.sizeSeries,
            GameManager.instance.editionSettings.currentEdition.namePlan,
            GameManager.instance.editionSettings.currentEdition.processSUSEP,
            GameManager.instance.editionSettings.currentEdition.commercialName,
            GameManager.instance.editionSettings.currentEdition.value,
            GameManager.instance.editionSettings.currentEdition.chances,
            GameManager.instance.editionSettings.currentEdition.modality,
            GameManager.instance.editionSettings.currentEdition.typeGlobe,
            GameManager.instance.editionSettings.currentEdition.lotteryInfos.Count,
            GameManager.instance.editionSettings.currentEdition.globeInfos.Count,
            GameManager.instance.editionSettings.currentEdition.spinInfos.Count
            );
    }

    public void PopulateEditionInfos(string _name, int _number, string _date, int _sizeSeries, 
        string _namePlan, string _processSUSEP, string _commercialName, float _value, string _chances, 
        int _modality,string _typeGlobe,int _amountLottery, int _amountGlobe, int _amoutSpin)
    {
        infosCards[0].SetInfo(_name);
        infosCards[1].SetInfo(_number.ToString());
        infosCards[2].SetInfo(_date);
        infosCards[3].SetInfo(_sizeSeries.ToString());
        infosCards[4].SetInfo(_namePlan);
        infosCards[5].SetInfo(_processSUSEP);
        infosCards[6].SetInfo(_commercialName);
        infosCards[7].SetInfo(GameManager.instance.FormatMoneyInfo(_value));
        infosCards[8].SetInfo(_typeGlobe);
        infosCards[9].SetInfo(_chances);
        infosCards[10].SetInfo(_modality.ToString());
        infosCards[11].SetInfo(_amountLottery.ToString());
        infosCards[11].SetInfo(_amountLottery.ToString());
        infosCards[12].SetInfo(_amountGlobe.ToString());
        infosCards[13].SetInfo(_amoutSpin.ToString());
    }

    public void ClearFields()
    {
        foreach (var item in infosCards)
        {
            item.ClearFieldInfo();
        }
    }
}
