using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiSelectEditionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownEditions;
    [SerializeField] private Button btSearch;
    [SerializeField] private List<EditionInfoCard> editionInfoCards;
    void Start()
    {
        btSearch.onClick.AddListener(GetInfosSelectedEdition);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += RequestInfos;
        NetworkManager.OnAllEditions += PopulateDropdown;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= RequestInfos;
        NetworkManager.OnAllEditions -= PopulateDropdown;
    }

    private void RequestInfos(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SelectEdition")
        {
            NetworkManager.instance.RequestAllEditions();
        }

    }

    private void PopulateDropdown()
    {
        dropdownEditions.ClearOptions();
        List<string> newOptions = new List<string>();

        for (int i = 0; i < GameManager.instance.editionSettings.allEditions.Count; i++)
        {
            string option = $"{GameManager.instance.editionSettings.allEditions[i].nome} - {GameManager.instance.editionSettings.allEditions[i].globoTipo}";
            newOptions.Add(option);
        }
        dropdownEditions.AddOptions(newOptions);
    }

    public void GetEditionIndex()
    {
        GameManager.instance.SetEditionIndex(dropdownEditions.value);
    }
    private void GetInfosSelectedEdition()
    {
        int index = GameManager.instance.EditionIndex;
        GameManager.instance.editionSettings.currentEdition = GameManager.instance.editionSettings.allEditions[index];
        editionInfoCards[0].SetInfo(GameManager.instance.editionSettings.currentEdition.nome);
        editionInfoCards[1].SetInfo(GameManager.instance.editionSettings.currentEdition.numero);
        editionInfoCards[2].SetInfo(GameManager.instance.editionSettings.currentEdition.nomePlano);
        editionInfoCards[3].SetInfo(GameManager.instance.editionSettings.currentEdition.processoSUSEP);
        editionInfoCards[4].SetInfo(GameManager.instance.editionSettings.currentEdition.denominacaoComercial);
        editionInfoCards[5].SetInfo(GameManager.instance.editionSettings.currentEdition.dataRealizacao);
        editionInfoCards[6].SetInfo(GameManager.instance.editionSettings.currentEdition.tipoTamanhoSerie.ToString(".000"));
        editionInfoCards[7].SetInfo(GameManager.instance.editionSettings.currentEdition.globoTipo);
        editionInfoCards[8].SetInfo(GameManager.instance.editionSettings.currentEdition.tipoQuantidadeChances);
        editionInfoCards[9].SetInfo(GameManager.instance.FormatMoneyInfo(GameManager.instance.editionSettings.currentEdition.valor));
       
    }
}
