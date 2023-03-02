using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeManager : MonoBehaviour
{
    [SerializeField] private BallsDrawnController ballsDrawnController;
    [SerializeField] private SelectBallController selectBallController;
    [SerializeField] private InfosCurrentDrawController infosCurrentDraw;

    [SerializeField] int max_balls = 60;
    void Start()
    {
        infosCurrentDraw.PopulateInfosCurrentDraw(1, "15 mil reais", 15000);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            ballsDrawnController.SetGridBalls(30);
            selectBallController.SetGridBalls(30);
            max_balls = 30;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ballsDrawnController.SetGridBalls(50);
            selectBallController.SetGridBalls(50);
            max_balls = 50;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ballsDrawnController.SetGridBalls(60);
            selectBallController.SetGridBalls(60);
            max_balls = 60;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            ballsDrawnController.SetGridBalls(75);
            selectBallController.SetGridBalls(75);
            max_balls = 75;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            ballsDrawnController.SetGridBalls(90);
            selectBallController.SetGridBalls(90);
            max_balls = 90;
        }
    }
}
