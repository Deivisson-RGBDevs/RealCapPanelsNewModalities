using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BallsDrawnController : MonoBehaviour
{
    [Header("COMPONENTS SPAWN")]
    [SerializeField] private GridLayoutGroup gridBallsDrawn;

    [Header("COMPONENTS")]
    [SerializeField] private BallDrawn ballDrawn;

    [SerializeField] int maxBalls = 60;
    [SerializeField] List<BallDrawn> ballsDrawn;

    void Start()
    {
        SetGridBalls(maxBalls);
    }
    private void ConfigGridBalls(int _cellSize, int _spacingX, int _spacingY, int _constraintCount)
    {
        gridBallsDrawn.cellSize = new Vector2(_cellSize, _cellSize);
        gridBallsDrawn.spacing = new Vector2(_spacingX, _spacingY);
        gridBallsDrawn.constraintCount = _constraintCount;
    }

    private void SpawnBgBalls(int _amountBalls)
    {
        ballsDrawn.Clear();
        for (int i = 0; i < _amountBalls; i++)
        {
            BallDrawn inst = Instantiate(ballDrawn, transform.position, Quaternion.identity);
            inst.transform.SetParent(gameObject.transform);
            ballsDrawn.Add(inst);
        }
    }

    private void ResetGrid()
    {   
        for (int i = 0; i < ballsDrawn.Count; i++)
        {
            Destroy(transform.GetChild(i).gameObject,0.1f);
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
                    ConfigGridBalls(80, 42, 20, 15);
                    break;
                }
            case 50:
                {
                    ConfigGridBalls(52, 22, 50, 25);
                    break;
                }
            case 60:
                {
                    ConfigGridBalls(52, 10, 50, 30);
                    break;
                }
            case 75:
                {
                    ConfigGridBalls(52, 22, 14, 25);
                    break;
                }
            case 90:
                {
                    ConfigGridBalls(52, 10, 14, 30);
                    break;
                }
        }
        SpawnBgBalls(_maxBalls);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
