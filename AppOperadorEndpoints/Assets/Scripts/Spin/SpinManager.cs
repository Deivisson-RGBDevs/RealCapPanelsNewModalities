using UnityEngine;

public class SpinManager : MonoBehaviour
{
    [SerializeField] private InfosCurrentDrawController infosCurrentDraw;
    [SerializeField] private ListSpinDraw listSpins;


    void Start()
    {
        infosCurrentDraw.PopulateInfosCurrentDraw(
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.indexSpinDraw].order,
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.indexSpinDraw].description,
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.indexSpinDraw].value);

        listSpins.PopulateListSpinDraw(GameManager.instance.editionSettings.currentEdition.spinInfos.Count);
        listSpins.ActiveCurrentDraw(GameManager.instance.indexSpinDraw);
    }

}
