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
        infosCurrentDraw.PopulateInfosCurrentDraw(1, "15 mil reais", 15000);
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
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            ballsDrawnController.SetGridBalls(30);
            selectBallController.SetGridBalls(30);
            maxBalls = 30;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ballsDrawnController.SetGridBalls(50);
            selectBallController.SetGridBalls(50);
            maxBalls = 50;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ballsDrawnController.SetGridBalls(60);
            selectBallController.SetGridBalls(60);
            maxBalls = 60;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            ballsDrawnController.SetGridBalls(75);
            selectBallController.SetGridBalls(75);
            maxBalls = 75;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            ballsDrawnController.SetGridBalls(90);
            selectBallController.SetGridBalls(90);
            maxBalls = 90;
        }
    }
}
