using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Globe Settings", menuName = "Raffle Settings/Globe Settings")]
public class GlobeScriptable : ScriptableObject
{
    public string editionName;
    public string editionNumber;
    public int order;
    public string date;
    public string description;
    public float value;
    [Space]
    public int Winners;
    public float prizeValue;
    public int ballRaffledCount;
    public int possiblesWinnersCount;
    [Space]
    public List<int> ballsDrawn;
    public int indexBalls = 0;
    public void ResetRaffle()
    {
        Winners = 0;
        prizeValue = 0;
        ballRaffledCount = 0;
        possiblesWinnersCount = 0;
        ballsDrawn.Clear();
        indexBalls = 0;
    }

    public void AddNewBall(int _ball)
    {
        ballsDrawn.Add(_ball);
    }
    public void RemoveBall(int _ball)
    {
        ballsDrawn.Remove(_ball);
    }
}

