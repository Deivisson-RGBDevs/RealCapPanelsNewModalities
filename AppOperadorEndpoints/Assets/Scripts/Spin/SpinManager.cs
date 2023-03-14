using UnityEngine;

public class SpinManager : MonoBehaviour
{
    [SerializeField] private InfosCurrentDrawController infosCurrentDraw;
    [SerializeField] private ListSpinDraw listSpins;
    [SerializeField] private int indexSpin;
    private void OnEnable()
    {
        TicketController.OnNextSpinDraw += UpdateSpinIndex;

    }
    private void OnDisable()
    {
        TicketController.OnNextSpinDraw -= UpdateSpinIndex;
    }

    void Start()
    {
        GameManager.instance.SetDrawMode(GameManager.DrawMode.Spin);
        UpdateCurrentDrawInfos();

        listSpins.PopulateListSpinDraw(GameManager.instance.editionSettings.currentEdition.spinInfos.Count);
        listSpins.ActiveNewDraw(GameManager.instance.currentSpinDraw-1);
    }

    private void UpdateSpinIndex()
    {
        if (GameManager.instance.currentSpinDraw < GameManager.instance.editionSettings.currentEdition.spinInfos.Count)
        {
            if (GameManager.instance.currentSpinDraw <= listSpins.CountFinishDraw())
            {
                GameManager.instance.currentSpinDraw++;
            }
            UpdateCurrentDrawInfos();
            ActiveNewSpinDraw(GameManager.instance.currentSpinDraw-1);
        }

    }

    private void UpdateCurrentDrawInfos()
    {
        infosCurrentDraw.PopulateInfosCurrentDraw(
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.currentSpinDraw-1].order,
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.currentSpinDraw-1].description,
            GameManager.instance.editionSettings.currentEdition.spinInfos[GameManager.instance.currentSpinDraw-1].value);
    }
    private void ActiveNewSpinDraw(int _index)
    {
        listSpins.ActiveNewDraw(_index);
    }
}
