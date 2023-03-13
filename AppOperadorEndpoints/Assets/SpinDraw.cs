using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpinDraw : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtSpinNumber;
    [SerializeField] private Button btDrawNumber;
    [SerializeField] private Button btShowTicket;
    [SerializeField] private CanvasGroup canvasGroup;


    public void SetActive(bool _isActive)
    {
        canvasGroup.interactable = _isActive;
        if(_isActive)
        {
            canvasGroup.alpha = 1f;
            txtSpinNumber.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0.5f;
            txtSpinNumber.alpha = 0.5f;
        }
    }
    public void PopulateTitle(int _index)
    {
        txtTitle.text = $"Sortear {_index}° Giro";
    }

    
    public void SetSpinNumber(int _number)
    {
        txtSpinNumber.text = _number.ToString();
    }
  
}
