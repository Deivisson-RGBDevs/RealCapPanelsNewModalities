using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectDrawController : MonoBehaviour
{
    [SerializeField] private FadeController fade;

    [SerializeField] private CanvasGroup CanvasSelectLottery;
    [SerializeField] private CanvasGroup CanvasSelectGlobe;
    [SerializeField] private CanvasGroup CanvasSelectSpin;

    void Start()
    {
        fade = FindObjectOfType<FadeController>();
        CheckModalities(GameManager.instance.editionSettings.currentEdition.modalidades);
    }

    private void CheckModalities(int modalitie)
    {
        switch (modalitie)
        {
            case 1:
                {
                    SetActiveCanvasGroup(CanvasSelectLottery, true);
                    SetActiveCanvasGroup(CanvasSelectGlobe, false);
                    SetActiveCanvasGroup(CanvasSelectSpin, false);
                    break;
                }
            case 2:
                {
                    SetActiveCanvasGroup(CanvasSelectLottery, true);
                    SetActiveCanvasGroup(CanvasSelectGlobe, true);
                    SetActiveCanvasGroup(CanvasSelectSpin, true);
                    break;
                }
            case 3:
                {
                    SetActiveCanvasGroup(CanvasSelectLottery, false);
                    SetActiveCanvasGroup(CanvasSelectGlobe, true);
                    SetActiveCanvasGroup(CanvasSelectSpin, true);
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

    // Update is called once per frame
    void Update()
    {

    }
}
