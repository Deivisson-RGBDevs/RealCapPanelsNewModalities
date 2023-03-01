using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectDrawController : MonoBehaviour
{
    [SerializeField] private FadeController fade;

    [SerializeField] private CanvasGroup canvasSelectLottery;
    [SerializeField] private CanvasGroup canvasSelectGlobe;
    [SerializeField] private CanvasGroup canvasSelectSpin;
    [Space]
    [SerializeField] private Button btSelectLottery;
    [SerializeField] private Button btSelectGlobe;
    [SerializeField] private Button btSelectSpin;


    void Start()
    {
        fade = FindObjectOfType<FadeController>();
        CheckModalities(GameManager.instance.editionSettings.currentEdition.modalidades);
        
        btSelectLottery.onClick.AddListener(GoToSceneLottery);
        btSelectGlobe.onClick.AddListener(GoToSceneGlobe);
        btSelectSpin.onClick.AddListener(GoToSceneSpin);
    }

    private void CheckModalities(int modalitie)
    {
        switch (modalitie)
        {
            case 1:
                {
                    SetActiveCanvasGroup(canvasSelectLottery, true);
                    SetActiveCanvasGroup(canvasSelectGlobe, false);
                    SetActiveCanvasGroup(canvasSelectSpin, false);
                    break;
                }
            case 2:
                {
                    SetActiveCanvasGroup(canvasSelectLottery, true);
                    SetActiveCanvasGroup(canvasSelectGlobe, true);
                    SetActiveCanvasGroup(canvasSelectSpin, true);
                    break;
                }
            case 3:
                {
                    SetActiveCanvasGroup(canvasSelectLottery, false);
                    SetActiveCanvasGroup(canvasSelectGlobe, true);
                    SetActiveCanvasGroup(canvasSelectSpin, true);
                    break;
                }
        }
    }

    private void SetActiveCanvasGroup(CanvasGroup _canvasGroup, bool _isActive)
    {
        _canvasGroup.interactable = _isActive;
        if (_isActive)
            _canvasGroup.alpha = 1.0f;
        else
            _canvasGroup.alpha = 0.5f;
    }

    private void GoToSceneLottery()
    {
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("Lottery");
    }
    private void GoToSceneGlobe()
    {
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("Globe");
    }
    private void GoToSceneSpin()
    {
        fade.SetStateFadeOUT();
        GameManager.instance.LoadSceneGame("Spin");
    }
}
