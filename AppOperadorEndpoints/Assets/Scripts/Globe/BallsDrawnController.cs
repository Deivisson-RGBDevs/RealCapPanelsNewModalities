using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallsDrawnController : MonoBehaviour
{
    [Header("COMPONENTS SPAWN")]
    [SerializeField] private GridLayoutGroup gridBallsDrawn;

    [Header("COMPONENTS")]
    [SerializeField] private BallDrawn ballDrawn;

    [SerializeField] int maxBalls = 60;
    [SerializeField] private List<BallDrawn> ballsDrawn;

    void Start()
    {
        SetGridBalls(maxBalls);
    }

    private void OnEnable()
    {
        GlobeManager.OnUpdateScreen += UpdateDrawnBalls;
    }
    private void OnDisable()
    {
        GlobeManager.OnUpdateScreen -= UpdateDrawnBalls;
    }

    private void UpdateDrawnBalls()
    {
        ClearAllCells();
        for (int i = 0; i < GameManager.instance.globeDrawnScriptable.bolasSorteadas.Count; i++)
        {
            ballsDrawn[i].SetNumberInText(GameManager.instance.globeDrawnScriptable.bolasSorteadas[i]);
        }
    }

    private void ClearAllCells()
    {
        for (int i = 0; i < ballsDrawn.Count; i++)
        {
            ballsDrawn[i].ClearCell();
        }
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
                    ConfigGridBalls(76, 50, 20, 15);
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
                    ConfigGridBalls(44, 32, 14, 25);
                    break;
                }
            case 90:
                {
                    ConfigGridBalls(44, 18, 14, 30);
                    break;
                }
        }
        SpawnBgBalls(_maxBalls);
    }
}
