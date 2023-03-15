using System;
using UnityEngine;

public class GlobeManager : MonoBehaviour
{
    public static event Action OnUpdateScreen;
    [SerializeField] private BallsDrawnController ballsDrawnController;
    [SerializeField] private SelectBallController selectBallController;
    [SerializeField] private InfosCurrentDrawController infosCurrentDraw;

    [SerializeField] int maxBalls = 60;
    void Start()
    {
        GameManager.instance.SetDrawMode(GameManager.DrawMode.Globe);

        infosCurrentDraw.PopulateInfosCurrentDraw(1, "15 mil reais", 15000);
        SetupMaxBalls(GameManager.instance.editionSettings.currentEdition.typeGlobe);
    }

    private void OnEnable()
    {
        PanelConfirmBall.OnConfirmSelection += UpdateListBallsDrawn;
    }
    private void OnDisable()
    {
        PanelConfirmBall.OnConfirmSelection -= UpdateListBallsDrawn;
    }

    private void UpdateListBallsDrawn(bool _hasRevocable, int _number)
    {
        if (_hasRevocable)
        {
            GameManager.instance.SetRemoveBall(_number);
        }
        else
            GameManager.instance.SetNewBall(_number);

        OnUpdateScreen?.Invoke();
    }
    public void SetupMaxBalls(string type)
    {
        switch(type)
        {
            case "15x30":
                {
                    ballsDrawnController.SetGridBalls(30);
                    selectBallController.SetGridBalls(30);
                    maxBalls = 30;
                    break;
                }
            case "15x50":
                {
                    ballsDrawnController.SetGridBalls(50);
                    selectBallController.SetGridBalls(50);
                    maxBalls = 50;
                    break;
                }
            case "20x60":
                {
                    ballsDrawnController.SetGridBalls(60);
                    selectBallController.SetGridBalls(60);
                    maxBalls = 60;
                    break;
                }
            case "15x75":
                {
                    ballsDrawnController.SetGridBalls(75);
                    selectBallController.SetGridBalls(75);
                    maxBalls = 75;
                    break;
                }
            case "15x90":
                {
                    ballsDrawnController.SetGridBalls(90);
                    selectBallController.SetGridBalls(90);
                    maxBalls = 90;
                    break;
                }
        }
    }
}
