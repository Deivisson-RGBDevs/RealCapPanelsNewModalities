using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManagerMainScene : MonoBehaviour
{
    [Header("UI INFOS")]
    [SerializeField] private UIInfosEdition uiInfosEdition;
    void Start()
    {
        uiInfosEdition.ShowTechnicalInfos(GameManager.instance.userSettings.tecnicoNome, GameManager.instance.userSettings.tecnicoCPF);

    }
    private void OnEnable()
    {
        UiInfosRaffle.OnActiveEditionInfos += PopulateEditionInfos;
    }
    private void OnDisable()
    {
        UiInfosRaffle.OnActiveEditionInfos -= PopulateEditionInfos;
    }
    private void PopulateEditionInfos()
    {
        uiInfosEdition.ShowEditionInfos(
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].name,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].number.ToString(),
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].date,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].namePlan,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].processSUSEP,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].commercialName,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].sizeSeries,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].modality,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].chances,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].value
            );
    }
}
