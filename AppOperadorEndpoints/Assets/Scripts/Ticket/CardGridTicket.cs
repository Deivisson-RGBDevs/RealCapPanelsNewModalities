using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridTicket : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridSelectBalls;

    [SerializeField] private List<CellCardTicket> cellCards;
    [SerializeField] private CellCardTicket cellCard;

    private void OnEnable()
    {   
        if (GameManager.instance.GetCurrentDrawMode() == GameManager.DrawMode.Globe)
            SetupMaxBalls(GameManager.instance.editionSettings.currentEdition.typeGlobe);
    }
    private void ConfigGridBalls(int _cellSizeX, int _cellSizeY, int _spacingX, int _spacingY, int _constraintCount)
    {
        gridSelectBalls.cellSize = new Vector2(_cellSizeX, _cellSizeY);
        gridSelectBalls.spacing = new Vector2(_spacingX, _spacingY);
        gridSelectBalls.constraintCount = _constraintCount;
    }
    public void SetupMaxBalls(string type)
    {
        switch (type)
        {
            case "15x30":
            case "15x50":
            case "15x75":
            case "15x90":
                {
                    SetGridBalls(15);
                    print("1");
                    break;
                }
            case "20x60":
                {
                    SetGridBalls(20);
                    print("2");
                    break;
                }
        }
    }

    public void SetGridBalls(int _maxBalls)
    {
        switch (_maxBalls)
        {
            case 15:
                {
                    ConfigGridBalls(60, 60, 10, 40, 3);
                    break;
                }
            case 20:
                {
                    ConfigGridBalls(60, 60, 10, 10, 4);
                    break;
                }
        }
        SpawnCellCards(_maxBalls);
    }

    private void SpawnCellCards(int _amountCards)
    {
        print("Teste   " + gameObject.name + "       Count" + cellCards.Count);
        ResetGrid();
        cellCards.Clear();
        for (int i = 0; i < _amountCards; i++)
        {
            CellCardTicket inst = Instantiate(cellCard, transform.position, Quaternion.identity);
            inst.transform.SetParent(gameObject.transform);
            cellCards.Add(inst);
        }
    }
    private void ResetGrid()
    {
        for (int i = 0; i < cellCards.Count; i++)
        {
            print("Deletou");
            Destroy(transform.GetChild(i).gameObject, 0.1f);
        }
    }
}
