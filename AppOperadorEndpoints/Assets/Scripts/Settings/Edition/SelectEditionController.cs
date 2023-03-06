using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectEditionController : MonoBehaviour
{
    [SerializeField] private FadeController fade;
    [Space]
    [SerializeField] private TMP_Dropdown dropdownEditions;
    [SerializeField] private Button btConfirm;
    [SerializeField] private List<EditionInfoCard> editionInfoCards;
    void Start()
    {
        btConfirm.onClick.AddListener(ConfirmSelectedEdition);

        fade = FindObjectOfType<FadeController>();
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
            string option = $"Edição {GameManager.instance.editionSettings.allEditions[i].number} - {GameManager.instance.editionSettings.allEditions[i].name}";
            newOptions.Add(option);
        }
        dropdownEditions.AddOptions(newOptions);
        GetInfosEdition();

    }

    public void GetInfosEdition()
    {
        GameManager.instance.SetEditionIndex(dropdownEditions.value);
        PopulateFieldsEdition();
    }
    private void PopulateFieldsEdition()
    {
        int index = GameManager.instance.EditionIndex;
        GameManager.instance.editionSettings.currentEdition = GameManager.instance.editionSettings.allEditions[index];
        editionInfoCards[0].SetInfo(GameManager.instance.editionSettings.currentEdition.name);
        editionInfoCards[1].SetInfo(GameManager.instance.editionSettings.currentEdition.number.ToString());
        editionInfoCards[2].SetInfo(GameManager.instance.editionSettings.currentEdition.date);
        editionInfoCards[3].SetInfo(GameManager.instance.ConvertIntToDec(GameManager.instance.editionSettings.currentEdition.sizeSeries,3).ToString(".000"));
        editionInfoCards[4].SetInfo(GameManager.instance.editionSettings.currentEdition.typeGlobe);
        editionInfoCards[5].SetInfo(GameManager.instance.editionSettings.currentEdition.chances);
        editionInfoCards[6].SetInfo(GameManager.instance.FormatMoneyInfo(GameManager.instance.editionSettings.currentEdition.value));
        btConfirm.interactable = true;
    }

    private void ConfirmSelectedEdition()
    {
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("SelectDraw");
    }


}
