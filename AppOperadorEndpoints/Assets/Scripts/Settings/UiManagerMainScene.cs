using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManagerMainScene : MonoBehaviour
{
    [Header("UI INFOS")]
    [SerializeField] private UIInfosEdition uiInfosEdition;
    [SerializeField] private LotteryController lotteryController;
    void Start()
    {

        lotteryController.PopulateLotteryFederalExtractions(
            GameManager.instance.lotteryScriptable.resultadoLoteriaFederalPrimeiroSorteio,
            GameManager.instance.lotteryScriptable.resultadoLoteriaFederalSegundoSorteio,
            GameManager.instance.lotteryScriptable.resultadoLoteriaFederalTerceiroSorteio,
            GameManager.instance.lotteryScriptable.resultadoLoteriaFederalQuartoSorteio,
            GameManager.instance.lotteryScriptable.resultadoLoteriaFederalQuintoSorteio
            );
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
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].nome,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].numero,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].dataRealizacao,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].nomePlano,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].processoSUSEP,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].denominacaoComercial,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].tipoTamanhoSerie,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].modalidades,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].tipoQuantidadeChances,
            GameManager.instance.editionSettings.allEditions[GameManager.instance.EditionIndex].valor
            );
    }
}
