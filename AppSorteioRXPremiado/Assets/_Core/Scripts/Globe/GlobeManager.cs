﻿using UnityEngine;
using System.Linq;

public class GlobeManager : MonoBehaviour
{

    [Header("Infos Screen")]
    [SerializeField] private TotalBallRaffle totalBallsCount;
    [SerializeField] private PossiblesWinners lastBallRaffle;

    [Header("CONTROLLERS")]
    [SerializeField] private BallController ballController;
    [SerializeField] private WinnersScreen winnersScreen;

    [Space]
    public bool isLoop = false;
    public bool isWinner = false;
    public bool hasShowHeart = true;
    [Space]
    public string numberSTR;
    public float timeToSpawn = 3f;
    public bool canRafleeBall = true;
    private bool isPlayWinnerSound = false;

    void Start()
    {
        InitializeVariables();
    }
    private void InitializeVariables()
    {
        isWinner = false;
        winnersScreen = FindObjectOfType<WinnersScreen>();
        //ballController.GetLastIndexBallsRaffle(GameManager.instance.globeScriptable.ballsDrawn.Count, GameManager.instance.globeScriptable.ballsDrawn);
        totalBallsCount.SetInfoTotalBall((GameManager.instance.globeScriptable.ballRaffledCount).ToString());
        lastBallRaffle.SetTicketForOneBallInfo(GameManager.instance.globeScriptable.possiblesWinnersCount.ToString());
        if (GameManager.instance.globeScriptable.possiblesWinnersCount > 0)
        {
            lastBallRaffle.PlayAnimationHeart(true);
        }
        else
        {
            lastBallRaffle.PlayAnimationHeart(false);
        }
        GameManager.instance.SetCamActiveInCanvas(Camera.main);
        isPlayWinnerSound = false;
    }
    private void OnEnable()
    {
        BallController.OnBallRaffled += VerifyWinner;
        BallController.OnBallDrawn += PermissionCallNewBallBall;
        GameManager.OnNewBallRecieve += VerifyBalls;
    }
    private void OnDisable()
    {
        GameManager.OnNewBallRecieve -= VerifyBalls;
        BallController.OnBallDrawn -= PermissionCallNewBallBall;
        BallController.OnBallRaffled -= SetUpdateInfoScreen;
    }
    public void PopulateInfosGlobe(string _editionName, string _editionNumber, string _date, int _order, string _description, float _value)
    {
        GameManager.instance.globeScriptable.editionName = _editionName;
        GameManager.instance.globeScriptable.editionNumber = _editionNumber;
        GameManager.instance.globeScriptable.date = _date;
        GameManager.instance.globeScriptable.order = _order;
        GameManager.instance.globeScriptable.description = _description;
        GameManager.instance.globeScriptable.value = _value;
    }

    public void PermissionCallNewBallBall()
    {
        canRafleeBall = true;
        GameManager.instance.globeScriptable.indexBalls++;
    }
    public void UpdateScreenRaffle(string[] _ballsRaffled, int _forOneBall, int _winnersCount, float _prizeValue)
    {
        if (GameManager.instance.globeScriptable.ballsDrawn.Count < _ballsRaffled.Length)
        {
            VerifyBalls();
            GameManager.instance.globeScriptable.ballsDrawn.Clear();
            //GameManager.instance.globeScriptable.ballsDrawn.AddRange(_ballsRaffled.ToList());

        }
        else if (GameManager.instance.globeScriptable.ballsDrawn.Count > _ballsRaffled.Length)
        {
            ballController.SetRevokedBall(_ballsRaffled);
            UpdateInfosScreen(_ballsRaffled.Length, _forOneBall);
        }

        if (_forOneBall > 0)
        {
            lastBallRaffle.PlayAnimationHeart(true);
        }
        else
        {
            lastBallRaffle.PlayAnimationHeart(false);
        }
        TicketScreen.instance.SetLastBallGlobeRaffle(_ballsRaffled[_ballsRaffled.Length - 1]);

        GameManager.instance.globeScriptable.Winners = _winnersCount;
        GameManager.instance.globeScriptable.prizeValue = _prizeValue;

        GameManager.instance.globeScriptable.ballRaffledCount = _ballsRaffled.Length;
        GameManager.instance.globeScriptable.possiblesWinnersCount = _forOneBall;
    }
    public void VerifyBalls()
    {
        if (GameManager.instance.globeScriptable.indexBalls < GameManager.instance.globeScriptable.ballsDrawn.Count)
            if (timeToSpawn <= 0.1f)
            {
                if (canRafleeBall == true)
                {
                    if (GameManager.instance.globeScriptable.ballsDrawn.Count <= 60)
                    {
                        StartCoroutine(ballController.ShowBigBall(GameManager.instance.globeScriptable.ballsDrawn[GameManager.instance.globeScriptable.indexBalls]));
                        timeToSpawn = 1f;
                        canRafleeBall = false;
                    }
                }
            }
        Invoke("VerifyBalls", 0.5f);
    }
    private void VerifyWinner()
    {
        if (GameManager.instance.globeScriptable.Winners > 0)
        {
            winnersScreen.SetInfosWinnerScreen(GameManager.instance.globeScriptable.Winners, GameManager.instance.globeScriptable.prizeValue);
            UiGlobeManager uiGlobeManager = FindObjectOfType<UiGlobeManager>();
            if (isPlayWinnerSound == false)
            {
                AudioManager.instance.PlaySFX("Winner");
                isPlayWinnerSound = true;
            }
        }
    }
    public void SetUpdateInfoScreen()
    {
        UpdateInfosScreen(GameManager.instance.globeScriptable.ballRaffledCount, GameManager.instance.globeScriptable.possiblesWinnersCount);
    }
    private void UpdateInfosScreen(int _totalNumberBalls, int _forOneBalls)
    {
        totalBallsCount.SetInfoTotalBall(_totalNumberBalls.ToString());
        lastBallRaffle.SetTicketForOneBallInfo(_forOneBalls.ToString());
    }
    private void FixedUpdate()
    {
        if (timeToSpawn > 0)
        {
            timeToSpawn -= Time.deltaTime;
        }
    }
}

