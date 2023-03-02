using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBallController : MonoBehaviour
{
    [Header("COMPONENTS SPAWN")]
    [SerializeField] private GridLayoutGroup gridSelectBalls;

    [Header("COMPONENTS")]
    [SerializeField] private SelectBall ballDrawn;

    [SerializeField] int maxBalls = 60;
    [SerializeField] List<SelectBall> ballsDrawn;

    void Start()
    {
        SetGridBalls(maxBalls);
    }
    private void SpawnBgBalls(int _amountBalls)
    {
        ballsDrawn.Clear();
        for (int i = 0; i < _amountBalls; i++)
        {
            SelectBall inst = Instantiate(ballDrawn, transform.position, Quaternion.identity);
            inst.transform.SetParent(gameObject.transform);
            int number = i + 1;
            inst.SetNumberInText(number);
            ballsDrawn.Add(inst);
        }
    }
    private void ConfigGridBalls(int _cellSizeX, int _cellSizeY, int _spacingX, int _spacingY, int _constraintCount)
    {
        gridSelectBalls.cellSize = new Vector2(_cellSizeX, _cellSizeY);
        gridSelectBalls.spacing = new Vector2(_spacingX, _spacingY);
        gridSelectBalls.constraintCount = _constraintCount;
    }
    private void ResetGrid()
    {
        for (int i = 0; i < ballsDrawn.Count; i++)
        {
            Destroy(transform.GetChild(i).gameObject, 0.1f);
        }
    }
    public void SetGridBalls(int _maxBalls)
    {
        ResetGrid();
        maxBalls = _maxBalls;
        switch (_maxBalls)
        {
            case 30:
                {
                    ConfigGridBalls(100, 100, 30, 50, 10);
                    break;
                }
            case 50:
                {
                    ConfigGridBalls(90, 70, 40, 20, 10);
                    break;
                }
            case 60:
                {
                    ConfigGridBalls(66, 66, 20, 50, 15);
                    break;
                }
            case 75:
                {
                    ConfigGridBalls(66, 66, 20, 25, 15);
                    break;
                }
            case 90:
                {
                    ConfigGridBalls(66, 52, 20, 24, 15);
                    break;
                }
        }
        SpawnBgBalls(_maxBalls);
    }
}
