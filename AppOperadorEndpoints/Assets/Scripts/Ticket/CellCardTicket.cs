using UnityEngine;
using TMPro;
public class CellCardTicket : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCellCard;

    public void PopulateTextCellCard(int _number)
    {
        txtCellCard.text = _number.ToString();
    }
  
}
